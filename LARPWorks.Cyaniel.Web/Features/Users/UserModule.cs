using LARPWorks.Cyaniel.Features;
using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class UserModule : CyanielModule
    {
        public UserModule() : base("Users")
        {
            Get["/dashboard"] = parameters => View["Dashboard.cshtml", GetViewModel<BaseCyanielViewModel>()];
        }
    }
}
