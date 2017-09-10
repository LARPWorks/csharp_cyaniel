using System.Collections.Generic;
using LARPWorks.Cyaniel.Data;
using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Characters.Downtime
{
    public class DowntimeModule : CyanielModule
    {
        public DowntimeModule() : base("characters/downtime")
        {
            Get["/index"] = parameters =>
            {
                var model = new DowntimeIndexViewModel(CurrentUser, new Character
                {
                    Name = "Edward Fitzdrake",
                    Id = 1,
                    UserId = 1,
                    Traits = new List<Trait>
                    {
                        new Trait { Name = "Retainer", Notes = "Trevor Beaumond", Type = TraitType.Perk },
                        new Trait { Name = "Retainer", Notes = "Arthur LeCarde", Type = TraitType.Perk }
                    }
                });
                return View["Index.cshtml", model];
            };
        }
    }
}
