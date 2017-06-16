using System.Collections.Generic;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class IndexViewModel : BaseCyanielViewModel
    {
        public Dictionary<string, List<BucketTicket>> Tickets { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public IndexViewModel()
        {
            Tickets = new Dictionary<string, List<BucketTicket>>()
            {
                {
                    "Character Submissions", new List<BucketTicket>
                    {
                        new BucketTicket { Title = "NEW CHARACTER: Evelyn Drake"},
                        new BucketTicket { Title = "NEW CHARACTER: Joe Blow"},
                        new BucketTicket { Title = "NEW CHARACTER: Fanta Orange" }
                    }
                },
                {
                    "Questions", new List<BucketTicket>
                    {
                        new BucketTicket { Title = "Is Kittens Fluffy?"},
                        new BucketTicket { Title = "How Am Fan Bath"},
                        new BucketTicket { Title = "Me Am Go Too Far????" }
                    }
                },
                {
                    "Bugs", new List<BucketTicket>
                    {
                        new BucketTicket { Title = "Scott is still an asshole"}
                    }
                }
            };
        }
    }
}
