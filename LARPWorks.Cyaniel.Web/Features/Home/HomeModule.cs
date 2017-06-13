using LARPWorks.Cyaniel.Web.Features.SharedViews;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Modules.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = parameters => View["Index.cshtml", new BaseCyanielViewModel()];
        }
    }
}
