using MySQL;

namespace LARPWorks.Cyaniel.Web.Features.SharedViews
{
    public class BaseCyanielViewModel
    {
        public User CurrentUser { get; set; }

        public virtual bool CanUserEdit(string field = "")
        {
            return false;
        }

        public bool IsAdmin()
        {
            return true;
        }
    }
}
