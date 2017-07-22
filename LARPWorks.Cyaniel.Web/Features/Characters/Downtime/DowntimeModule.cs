using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Characters.Downtime
{
    public class DowntimeModule : CyanielModule
    {
        public DowntimeModule() : base("characters/downtime")
        {
            Get["/index"] = parameters =>
            {
                return View["Index.cshtml", new DowntimeIndexViewModel("Edward Fitzdrake", 1, 1)];
            };
        }
    }
}
