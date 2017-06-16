using LARPWorks.Cyaniel.Web.Features.SharedViews;
using MySQL;
using System;

namespace LARPWorks.Cyaniel.Web.Features.Buckets
{
    public class CreateViewModel : BaseCyanielViewModel
    {
        public string TicketSubject { get; set; }
        public string Description { get; set; }
        public string Bucket { get; set; }
      
        public MySQL.Bucket[] BucketChoices { get; set; }

        public BucketTicket BuildBucketTicket(User user)
        {
            var bucketId = -1;
            foreach (MySQL.Bucket bucket in BucketChoices)
            {
                if (bucket.Name == Bucket) {
                    bucketId = bucket.Id;
                }
            }
            //TODO: Throw error if bucketId is -1 here.
            if (bucketId == -1)
            {
                System.Console.WriteLine("BucketID is -1, No Bucket Found for BucketTicket Post");
            }

            return new BucketTicket
            {
                Title = TicketSubject,
                BucketId = bucketId,
                CreatorId = user.Id,
                AssigneeId = null,
                Status = (int)Models.BucketTicketStatusEnum.Unclaimed,
                CreatedOn = DateTime.Now,
                LastModified = DateTime.Now
            };
        }

        public TicketComment BuildInitialComment(int TicketId, User user)
        {
            return new TicketComment
            {
                TicketId = TicketId,
                AuthorUserId = user.Id,
                Comment = Description,
                CreatedOn = DateTime.Now
            };
        }

        public TicketAccessList BuildInitialAccessList(int TicketId, User user)
        {
            return new TicketAccessList
            {
                TicketId = TicketId,
                UserId = user.Id,
                CanWrite = true,
                CanRead = true
            };

        }

        public CreateViewModel()
        {
            BucketChoices = new MySQL.Bucket[0];
        }
    }
}
