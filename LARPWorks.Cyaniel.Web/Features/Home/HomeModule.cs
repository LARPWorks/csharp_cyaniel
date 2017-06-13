using LARPWorks.Cyaniel.Web.Features.SharedViews;
using LARPWorks.Cyaniel.Web.Features.Users.Authentication;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Modules.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {   
            Get["/"] = parameters =>
            {
                var viewModel = new BaseCyanielViewModel();

                var user = Context.CurrentUser as UserIdentity;
                if (user != null)
                {
                    viewModel.CurrentUser = user.GetUser();
                }
                
                return View["Index.cshtml", viewModel];
            };
        }
    }
}
