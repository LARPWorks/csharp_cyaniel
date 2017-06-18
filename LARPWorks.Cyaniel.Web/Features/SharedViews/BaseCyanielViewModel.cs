using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Factories;

namespace LARPWorks.Cyaniel.Features.SharedViews
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

        public virtual void LoadFromDatabase(IDbFactory dbFactory)
        {
            
        }
    }
}
