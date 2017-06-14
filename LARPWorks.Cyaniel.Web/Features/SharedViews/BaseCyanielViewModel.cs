using MySQL;

namespace LARPWorks.Cyaniel.Web.Features.SharedViews
{
    public class BaseCyanielViewModel
    {
        public User CurrentUser { get; set; }

        public bool CanUserEdit()
        {
            return false;
        }
    }
}
