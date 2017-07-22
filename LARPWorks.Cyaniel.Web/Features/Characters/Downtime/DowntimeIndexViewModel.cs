using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LARPWorks.Cyaniel.Features.SharedViews;
using Newtonsoft.Json;

namespace LARPWorks.Cyaniel.Features.Characters.Downtime
{
    public class DowntimeIndexViewModel : BaseCyanielViewModel
    {
        public LeadershipNode LeadershipGraph { get; set; }
        public List<DowntimeAction> Actions { get; set; }

        public DowntimeIndexViewModel(string characterName, int userId, int characterId)
        {
            Actions = new List<DowntimeAction>();

            LeadershipGraph = new LeadershipNode
            {
                Id = 1,
                CharacterId = characterId,
                CharacterName = characterName
            };
            LeadershipGraph.Children.Add(new LeadershipNode
            {
                Id = 2,
                CharacterName = "Trevor Beaumond",
                CharacterId = characterId,
                IsRetainer = true
            });
            LeadershipGraph.Children.Add(new LeadershipNode
            {
                Id = 3,
                CharacterName = "Arthur LeCarde",
                CharacterId = characterId,
                IsRetainer = true
            });
            LeadershipGraph.Leaders.Add(new LeadershipNode
            {
                Id = 4,
                CharacterName = "Adelaide Drake",
                CharacterId = 5
            });
            LeadershipGraph.Leaders.Add(new LeadershipNode
            {
                Id = 5,
                CharacterName = "Thomas Drake",
                CharacterId = 6
            });
            LeadershipGraph.Leaders.Add(new LeadershipNode
            {
                Id = 6,
                CharacterName = "Weiland",
                CharacterId = 7
            });

            Actions.Add(new DowntimeAction
            {
                Id = 1,
                CharacterName = characterName,
                CharacterId = characterId,
                OwnerUserId = userId,
            });
            Actions.Add(new DowntimeAction
            {
                Id = 2,
                CharacterName = "Trevor Beaumond",
                CharacterId = characterId,
                OwnerUserId = userId,
                FromRetainer = true
            });
            Actions.Add(new DowntimeAction
            {
                Id = 3,
                CharacterName = "Arthur LeCarde",
                CharacterId = characterId,
                OwnerUserId = userId,
                AssignedToCharacterId = 5,
                AssignedtoUserId = 5,
                FromRetainer = true
            });
        }
    }

    public class LeadershipNode
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public List<LeadershipNode> Leaders { get; set; }
        public List<LeadershipNode> Children { get; set; }
        [JsonProperty(PropertyName = "label")]
        public string CharacterName { get; set; }
        public int OwnerUserId { get; set; }
        public int CharacterId { get; set; }
        public bool IsRetainer { get; set; }

        public LeadershipNode()
        {
            Leaders = new List<LeadershipNode>();
            Children = new List<LeadershipNode>();
        }
    }

    public class DowntimeAction
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int OwnerUserId { get; set; }
        public int CharacterId { get; set; }
        public bool FromRetainer { get; set; }
        public int AssignedToCharacterId { get; set; }
        public int AssignedtoUserId { get; set; }
    }
}
