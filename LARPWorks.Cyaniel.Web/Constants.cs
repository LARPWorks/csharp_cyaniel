using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * This is a file dedicated to the constants of the cyaniel application.
 */
namespace LARPWorks.Cyaniel
{
    public enum BucketTicketStatusEnum : ushort
    {
        Unclaimed = 0,
        RequiresAttention = 1,
        InProgress = 2,
        Complete = 3
    }
}
