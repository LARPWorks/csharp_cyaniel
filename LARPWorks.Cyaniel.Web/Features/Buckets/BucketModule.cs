using LARPWorks.Cyaniel.Web.Features.Buckets;
using LARPWorks.Cyaniel.Web.Models.Factories;

namespace LARPWorks.Cyaniel.Web.Features.Bucket
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
        }

        private CreateViewModel BuildModel()
        {
            var baseModel = GetViewModel<CreateViewModel>();
            using (var db = _dbFactory.Create())
            {
                var buckets = db.Fetch<MySQL.Bucket>("SELECT * FROM Buckets");
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
