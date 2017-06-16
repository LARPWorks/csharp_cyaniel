using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class TicketViewModel : CreateViewModel
    {
        public string Creator { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }

        public TicketViewModel()
        {
            Creator = "biscuitWizard";
            Assignee = "Scott Spalding";
            Status = BucketTicketStatusEnum.Unclaimed.ToString();
        }
    }
}
