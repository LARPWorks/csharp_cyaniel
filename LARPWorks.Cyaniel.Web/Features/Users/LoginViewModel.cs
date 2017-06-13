using LARPWorks.Cyaniel.Web.Features.SharedViews;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class LoginViewModel : BaseCyanielViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
