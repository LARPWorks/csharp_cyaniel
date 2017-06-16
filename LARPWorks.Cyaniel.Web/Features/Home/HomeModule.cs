using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Home
{
    public class HomeModule : CyanielModule
    {
        public HomeModule()
        {   
            Get["/"] = parameters => View["Index.cshtml", GetViewModel<BaseCyanielViewModel>()];
        }
    }
}
