using LARPWorks.Cyaniel.Web.Features.SharedViews;

namespace LARPWorks.Cyaniel.Web.Features.Characters
{
    public class CharacterViewModel : BaseCyanielViewModel
    {
        public string[] Sections { get; set; }

        public CharacterViewModel()
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
        }
    }
}
