using System.Collections.Generic;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models.Characters;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterSheetViewModel : BaseCyanielViewModel
    {
        public CharacterModel Character { get; set; }

        public string[] Sections { get; set; }

        public GameStatisticModel[] Skills { get; set; }
        public GameStatisticModel[] Attributes { get; set; }
        public GameStatisticModel[] Esoterics { get; set; }
        public GameStatisticModel[] Perks { get; set; }
        public GameStatisticModel[] Flaws { get; set; }
        public GameStatisticModel[] Exoterics { get; set; }
        public GameStatisticModel[] FamilyJobAndHometownSkills { get; set; }
        public GameStatisticModel[] Cultures { get; set; }
        public GameStatisticModel[] SocialStatuses { get; set; }
        public Dictionary<string, GameStatisticModel[]> CultureSkills { get; set; }
        public Dictionary<string, GameStatisticModel[]> SocialStatusSkills { get; set; }
        
        public CharacterSheetViewModel()
        {
            Sections = new []
            {
                "Character Sheet",
                "Attributes & Skills",
                "Perks & Flaws",
                "Foci & Languages",
                "Maneuvers",
                "Priest Rituals",
                "Magic",
                "Knight Rank & Oaths",
                "Paladin Powers",
                "Leadership",
                "Biography",
                "Notes"
            };

            Character = new CharacterModel();
        }

        public string SanitizeSectionName(string sectionName)
        {
            return sectionName.Replace(" ", string.Empty).Replace("&", string.Empty);
        }
    }
}
