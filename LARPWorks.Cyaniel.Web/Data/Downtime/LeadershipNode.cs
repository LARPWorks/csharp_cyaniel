using System.Collections.Generic;
using Newtonsoft.Json;

namespace LARPWorks.Cyaniel.Data.Downtime
{
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
}
