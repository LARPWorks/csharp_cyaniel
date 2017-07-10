using System;
using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class BucketModule : CyanielModule
    {
        private readonly IDbFactory _dbFactory;
        public BucketModule(IDbFactory dbFactory) : base("Buckets")
        {
            _dbFactory = dbFactory;

            Get["/index"] = parameters => View["Index.cshtml", GetIndexModel()];
            Post["/create"] = parameters =>
            {
                var model = GetViewModel<CreateViewModel>();
                string newTicketId = string.Empty;

                using (var db = dbFactory.Create())
                {
                    var bucket = db.SingleOrDefault<Bucket>("SELECT * FROM Buckets WHERE Name=@0",
                        model.Bucket);

                    var ticket = new BucketTicket
                    {
                        Title = model.Title,
                        Description = model.Description,
                        CreatorId = model.CurrentUser.Id,
                        BucketId = bucket.Id,
                        Priority = (int)Enum.GetValues(typeof(BucketTicketPriorityEnum)).Cast<BucketTicketPriorityEnum>()
                            .First(b => b.ToString() == model.Priority),
                        CreatedOn = DateTime.Now,
                        LastModified = DateTime.Now
                    };

                    newTicketId = db.Insert(ticket).ToString();
                }

                return Response.AsRedirect(string.Format("/buckets/view/{0}", Int32.Parse(newTicketId)));
            };

            Get["/view/{Id:int}"] = parameters =>
            {
                int ticketId = (int) parameters.Id;
                var ticketView = GetTicket(ticketId);

                return View["View.cshtml", ticketView];
            };
            Post["/view/{Id:int}"] = parameters =>
            {
                int ticketId = (int)parameters.Id;
                return Response.AsRedirect(string.Format("/buckets/view/{0}", ticketId));
            };
        }

        private TicketViewModel GetTicket(int id)
        {
            var baseModel = GetViewModel<TicketViewModel>();
            using (var db = _dbFactory.Create())
            {
                var ticket = db.SingleOrDefault<BucketTicket>("SELECT * FROM BucketTickets WHERE Id=@0", id);
                if (ticket == null)
                {
                    return baseModel;
                }

                baseModel.FromModel(ticket, db);
            }

            return baseModel;
        }
        
        private IndexViewModel GetIndexModel()
        {
            var model = GetViewModel<IndexViewModel>();
            using(var db = _dbFactory.Create())
            {
                var buckets = db.Fetch<Bucket>("SELECT * FROM Buckets");
                model.Buckets = buckets.Select(b => b.Name).ToList();

                var tickets = db.Fetch<BucketTicket>("SELECT * FROM BucketTickets");

                foreach (var ticket in tickets)
                {
                    ticket.Bucket = buckets.First(b => b.Id == ticket.BucketId).Name;
                    if (ticket.AssigneeId.HasValue)
                    {
                        ticket.AssigneeUser =
                            db.SingleOrDefault<User>("SELECT * FROM Users WHERE Id=@0", ticket.AssigneeId).Username;
                    }
                }

                foreach (var bucket in buckets)
                {
                    model.Tickets.Add(bucket.Name, tickets.Where(t => t.BucketId == bucket.Id).ToList());
                }
            }

            return model;
        }
    }
}
