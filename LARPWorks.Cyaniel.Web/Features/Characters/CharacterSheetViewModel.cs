﻿using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models.Characters;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterSheetViewModel : BaseCyanielViewModel
    {
        public CharacterModel Character { get; set; }

        public string[] Sections { get; set; }

        public string[] Skills { get; set; }
        public string[] Attributes { get; set; }

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
