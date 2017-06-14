﻿using LARPWorks.Cyaniel.Web.Features.SharedViews;
using LARPWorks.Cyaniel.Web.Models;

namespace LARPWorks.Cyaniel.Web.Features.Buckets
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