using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Factories;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class BucketModule : CyanielModule
    {
        private readonly IDbFactory _dbFactory;
        public BucketModule(IDbFactory dbFactory) : base("Buckets")
        {
            _dbFactory = dbFactory;

            Get["/index"] = parameters => View["Index.cshtml", GetViewModel<IndexViewModel>()];
            Get["/create"] = parameters => View["Create.cshtml", BuildModel()];
            Post["/create"] = parameters =>
            {
                return View["Create.cshtml", GetViewModel<CreateViewModel>()];
            };
            var vm = GetViewModel<TicketViewModel>();
            vm.TicketSubject = "Test terst test";
            Get["/view"] = parameters => View["View.cshtml", vm];
            Post["/view"] = parameters => View["View.cshtml", vm];
        }

        private CreateViewModel BuildModel()
        {
            var baseModel = GetViewModel<CreateViewModel>();
            using (var db = _dbFactory.Create())
            {
                var buckets = db.Fetch<Bucket>("SELECT * FROM Buckets");
                if (buckets == null)
                {
                    return baseModel;
                }

                baseModel.BucketChoices = buckets.ToArray();
            }

            return baseModel;
        }
    }
}
