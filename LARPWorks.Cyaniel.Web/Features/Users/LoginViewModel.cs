using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Users
{
    public class LoginViewModel : BaseCyanielViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
