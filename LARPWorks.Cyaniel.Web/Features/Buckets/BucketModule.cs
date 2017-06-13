using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARPWorks.Cyaniel.Web.Features.Bucket
{
    public class BucketModule : CyanielModule
    {
        public BucketModule() : base("Buckets")
        {
            Get["/index"] = parameters => View["Index.cshtml", GetViewModel<IndexViewModel>()];
        }
    }
}
