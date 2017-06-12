using LARPWorks.Cyaniel.Web.Features.SharedViews;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule() : base("Users")
        {
            Get["/register"] = parameters => View["Register.cshtml", new BaseCyanielViewModel()];
        }
    }
}
