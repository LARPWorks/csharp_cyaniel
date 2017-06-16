using LARPWorks.Cyaniel.Web.Features.Buckets;
using LARPWorks.Cyaniel.Web.Features.SharedViews;
using LARPWorks.Cyaniel.Web.Models.Factories;
using LARPWorks.Cyaniel.Web.Features.Users.Authentication;

using Nancy;
using Nancy.ModelBinding;
using System;
using System.Linq;

namespace LARPWorks.Cyaniel.Web.Features.Bucket
{
    public class BucketModule : CyanielModule
    {
        private readonly IDbFactory _dbFactory;
        public BucketModule(IDbFactory dbFactory) : base("Buckets")
        {
            _dbFactory = dbFactory;

            Get["/index"] = parameters => View["Index.cshtml", GetViewModel<IndexViewModel>()];
            var createViewModel = BuildModel();
            var baseModel = new BaseCyanielViewModel();
            Get["/create"] = parameters => View["Create.cshtml", createViewModel];
            Post["/create"] = parameters =>
            {
                var userIdentity = Context.CurrentUser as UserIdentity;
                MySQL.User user = userIdentity.GetUser();
                createViewModel = this.Bind<CreateViewModel>();
                createViewModel = AddBucketChoices(createViewModel);
                baseModel = this.Bind<BaseCyanielViewModel>();
                SaveNewTicket(createViewModel, user);
                return View["Create.cshtml", GetViewModel<CreateViewModel>()];
            };
            var vm = GetViewModel<TicketViewModel>();
            
            vm.TicketSubject = "Test terst test";
            Get["/view/{ticketID}"] = parameters =>
            {
                var ticketId = parameters.ticketID;
                MySQL.BucketTicket ticket = GetTicket(ticketId);
                if (ticket == null)
                {
                    //TODO ticket not found, raise error

                }

                var comments = GetComments(ticketId);
                var BucketName = GetBucketName(ticket.BucketId);
                vm.Ticket = ticket;
                vm.Comments = comments;
                vm.BucketName = BucketName;
                vm.Status = Enum.GetName(typeof(Models.BucketTicketStatusEnum), ticket.Status);
                return View["View.cshtml", vm];
            };
            Post["/view"] = parameters => View["View.cshtml", vm];
        }

        private CreateViewModel BuildModel()
        {
            var baseModel = GetViewModel<CreateViewModel>();
            baseModel = AddBucketChoices(baseModel);
            return baseModel;
        }

        private CreateViewModel AddBucketChoices(CreateViewModel createViewModel)
        {
            using (var db = _dbFactory.Create())
            {
                var buckets = db.Fetch<MySQL.Bucket>("SELECT * FROM Buckets");
                if (buckets == null)
                {
                    return createViewModel;
                }

                createViewModel.BucketChoices = buckets.ToArray();
                return createViewModel;
            }
        }

        private MySQL.BucketTicket GetTicket(int ticketId)
        {
            using (var db = _dbFactory.Create())
            {
                return db.SingleOrDefault<MySQL.BucketTicket>("SELECT * FROM buckettickets where Id =@0", ticketId.ToString());

            }
        }
        private string GetBucketName(int bucketId)
        {
            using (var db = _dbFactory.Create())
            {
                return db.ExecuteScalar<string>("SELECT Name from buckets where Id = @0", bucketId);
            }
        }

        private MySQL.TicketComment[] GetComments(int ticketId)
        {
            using (var db = _dbFactory.Create())
            {
                var comments = db.Query<MySQL.TicketComment>(PetaPoco.Sql.Builder
                    .Append("SELECT * FROM ticketcomments")
                    .Append("where TicketId =@0 ", ticketId.ToString())
                    );

                if (comments == null)
                {
                    return null;
                }
                return comments.ToArray();
            }

        }
        private void SaveNewTicket(CreateViewModel createViewModel, MySQL.User user)
            //TODO: Needs reporting of bad input to View
        {
            var ticket = createViewModel.BuildBucketTicket(user);
            var savedTicketId = SaveTicketDB(ticket);
            if (savedTicketId == -1)
            {
                return;
            }
            var ticketComments = createViewModel.BuildInitialComment(savedTicketId, user);
            var ticketAccessList = createViewModel.BuildInitialAccessList(savedTicketId, user);
            try
            {
                using (var db = _dbFactory.Create())
                {
                    db.Insert(ticketComments);
                    db.Insert(ticketAccessList);
                }
            }
            catch (Exception e)
            {
                //TODO:Logging stuff
            }
        }

        private int SaveTicketDB(MySQL.BucketTicket ticket)
        {
            var id = -1;
            try
            {
                using (var db = _dbFactory.Create())
                {
                    db.Insert(ticket);
                    id = ticket.Id;
                }
            }
            catch (Exception e)
            {
                //TODO: Logging Stuff
            }
            return id;
        }
    }
}
