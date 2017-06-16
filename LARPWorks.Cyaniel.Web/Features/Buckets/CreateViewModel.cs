using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class CreateViewModel : BaseCyanielViewModel
    {
        public string TicketSubject { get; set; }
        public string Description { get; set; }
        public string Bucket { get; set; }

        public Bucket[] BucketChoices { get; set; }

        public CreateViewModel()
        {
            BucketChoices = new Bucket[0];
        }
    }
}
