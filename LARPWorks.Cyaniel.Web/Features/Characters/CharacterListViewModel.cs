using System.Collections.Generic;
using LARPWorks.Cyaniel.Features.SharedViews;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterListViewModel : BaseCyanielViewModel
    {
        public List<CharacterViewModel> Characters { get; set; }

        public CharacterListViewModel()
        {
            Characters = new List<CharacterViewModel>();
        }
    }
}
