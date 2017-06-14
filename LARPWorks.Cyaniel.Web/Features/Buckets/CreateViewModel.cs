using LARPWorks.Cyaniel.Web.Features.SharedViews;

namespace LARPWorks.Cyaniel.Web.Features.Buckets
{
    public class CreateViewModel : BaseCyanielViewModel
    {
        public string TicketSubject { get; set; }
        public string Description { get; set; }
        public string Bucket { get; set; }

        public MySQL.Bucket[] BucketChoices { get; set; }

        public CreateViewModel()
        {
            BucketChoices = new MySQL.Bucket[0];
        }
    }
}
