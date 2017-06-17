using System.Collections.Generic;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models.Characters;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterListViewModel : BaseCyanielViewModel
    {
        public List<CharacterModel> Characters { get; set; }
        public string NewCharacterName { get; set; }

        public CharacterListViewModel()
        {
            Characters = new List<CharacterModel>();
        }
    }
}
