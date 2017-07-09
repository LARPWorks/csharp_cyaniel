using System;
using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Models;

namespace LARPWorks.Cyaniel.Features.Buckets
{
    public class CommentViewModel
    {
        public string Author { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class TicketViewModel : CreateViewModel
    {
        public string Creator { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public TicketViewModel()
        {
            Creator = "Anomalous";
            Assignee = "Anomalous";
            Status = BucketTicketStatusEnum.Unclaimed.ToString();
            Comments = new List<CommentViewModel>();
        }

        public void FromModel(BucketTicket ticket, Database db)
        {
            var creator = db.SingleOrDefault<User>("SELECT * FROM Users WHERE Id=@0", ticket.CreatorId);
            var assignee = ticket.AssigneeId.HasValue
                ? db.SingleOrDefault<User>("SELECT * FROM Users WHERE Id=@0", ticket.AssigneeId)
                : null;

            Creator = creator.Username;
            Assignee = assignee == null ? "Unassigned" : assignee.Username;
            Status = ((BucketTicketStatusEnum)ticket.Status).ToString();
            Description = ticket.Description;
            TicketSubject = ticket.Title;

            var comments = db.Fetch<TicketComment>("SELECT * FROM TicketComments WHERE TicketId=@0", ticket.Id);
            var commentAuthors = new List<User>();
            if (comments.Count > 0)
            {
                commentAuthors = db.Fetch<User>(Sql.Builder
                    .Select("*")
                    .From("Users")
                    .Where("Id IN (@ids)",
                        new {ids = comments.Select(s => s.AuthorUserId).ToArray()}));
            }
            foreach (var comment in comments)
            {
                var author = commentAuthors.First(ca => ca.Id == comment.AuthorUserId);
                var ticketComment = new CommentViewModel
                {
                    Author = author.Username,
                    Comment = comment.Comment,
                    CreatedOn = comment.CreatedOn
                };

                Comments.Add(ticketComment);
            }
        }

        public BucketTicket ToModel()
        {
            return null;
        }
    }
}
