using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.SharedViews
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
