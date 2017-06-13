using LARPWorks.Cyaniel.Web.Features.SharedViews;

namespace LARPWorks.Cyaniel.Web.Features.Home
{
    public class HomeModule : CyanielModule
    {
        public HomeModule()
        {   
            Get["/"] = parameters => View["Index.cshtml", GetViewModel<BaseCyanielViewModel>()];
        }
    }
}
