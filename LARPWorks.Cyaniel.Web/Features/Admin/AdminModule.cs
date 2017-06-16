using LARPWorks.Cyaniel.Features;
using LARPWorks.Cyaniel.Features.SharedViews;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Features.Admin
{
    public class AdminModule : CyanielModule
    {
        public AdminModule() : base("admin")
        {
            Get["/dashboard"] = parameters => View["Dashboard.cshtml", GetViewModel<BaseCyanielViewModel>()];
        }
    }
}
