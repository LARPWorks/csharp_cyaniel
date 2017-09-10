using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Data;
using LARPWorks.Cyaniel.Data.Downtime;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;
using Character = LARPWorks.Cyaniel.Data.Character;

namespace LARPWorks.Cyaniel.Features.Characters.Downtime
{
    public class DowntimeIndexViewModel : BaseCyanielViewModel
    {
        public Character Character { get; set; }

        public LeadershipNode LeadershipGraph { get; set; }
        public List<DowntimeAction> Actions { get; set; }

        private int _lastLeadershipNodeId;

        public DowntimeIndexViewModel(User currnetUser, Character character)
        {
            CurrentUser = currnetUser;
            Actions = new List<DowntimeAction>
            {
                new DowntimeAction
                {
                    Id = GetNextNodeId(),
                    CharacterName = character.Name,
                    CharacterId = character.Id,
                    OwnerUserId = CurrentUser.Id
                }
            };

            LeadershipGraph = new LeadershipNode
            {
                Id = GetNextNodeId(),
                CharacterId = character.Id,
                CharacterName = character.Name
            };

            foreach (var retainer in character.Traits.Where(t => t.Type == TraitType.Perk && t.Name == "Retainer"))
            {
                var retainerNode = new LeadershipNode
                {
                    Id = GetNextNodeId(),
                    CharacterName = retainer.Notes,
                    CharacterId = character.Id,
                    IsRetainer = true
                };

                var retainerAction = new DowntimeAction
                {
                    Id = GetNextNodeId(),
                    CharacterId = character.Id,
                    CharacterName = retainer.Notes,
                    FromRetainer = true,
                    OwnerUserId = CurrentUser.Id
                };

                Actions.Add(retainerAction);
                LeadershipGraph.Children.Add(retainerNode);
            }
        }

        private int GetNextNodeId()
        {
            _lastLeadershipNodeId++;
            return _lastLeadershipNodeId;
        }
    }
}
