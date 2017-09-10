using System.Collections.Generic;
using LARPWorks.Cyaniel.Data;
using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterListViewModel : BaseCyanielViewModel
    {
        public List<Character> Characters { get; set; }
        public string NewCharacterName { get; set; }

        public CharacterListViewModel()
        {
            Characters = new List<Character>();
        }
    }
}
