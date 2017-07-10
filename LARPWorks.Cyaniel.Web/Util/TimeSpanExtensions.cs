using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARPWorks.Cyaniel.Util
{
    public static class TimeSpanExtensions
    {
        public static string ToAgeFormat(this TimeSpan timeSpan)
        {
            if (timeSpan.Days > 0)
            {
                return string.Format("{0:d} Days, {0:%h} Hours", timeSpan);
            } else if (timeSpan.Hours > 0)
            {
                return string.Format("{0:%h} Hours, {0:%m} Minutes", timeSpan);
            } else if (timeSpan.Minutes > 0)
            {
                return string.Format("{0:%m} Minutes, {0:%s} Seconds", timeSpan);
            } else if (timeSpan.Seconds > 15)
            {
                return string.Format("{0:%s} Seconds", timeSpan);
            }

            return "Just Now";
        }
    }
}
