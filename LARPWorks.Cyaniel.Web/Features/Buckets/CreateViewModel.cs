using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class CreateViewModel : BaseCyanielViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Bucket { get; set; }
        public string Priority { get; set; }
    }
}
