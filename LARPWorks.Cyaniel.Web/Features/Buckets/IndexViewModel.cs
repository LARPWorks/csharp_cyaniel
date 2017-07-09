using System.Collections.Generic;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class IndexViewModel : BaseCyanielViewModel
    {
        public Dictionary<string, List<BucketTicket>> Tickets { get; set; }
        public List<string> Buckets { get; set; } 
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public IndexViewModel()
        {
            Tickets = new Dictionary<string, List<BucketTicket>>();
            Buckets = new List<string>();
        }
    }
}
