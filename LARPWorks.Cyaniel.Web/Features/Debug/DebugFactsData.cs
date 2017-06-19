using System.Collections.Generic;

namespace LARPWorks.Cyaniel.Features.Debug
{
#if DEBUG
    public class DebugFactsData
    {
        public Dictionary<string, string[]> Facts;
        public Dictionary<string, string[]> AdvancementLists;
        public Dictionary<string, string[]> SocialStatusSkills;
        public Dictionary<string, string[]> CultureSkills;
        public Dictionary<string, string[]> EsotericGates;
    }
#endif
}
