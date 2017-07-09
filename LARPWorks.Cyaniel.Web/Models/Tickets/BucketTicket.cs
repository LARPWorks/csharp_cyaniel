using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARPWorks.Cyaniel.Models
{
    public partial class BucketTicket
    {
        public string AuthorUser { get; set; }
        public string AssigneeUser { get; set; }
        public string Bucket { get; set; }
        public List<TicketComment> Comments { get; set; }
    }
}
