using System.Collections.Generic;

namespace LARPWorks.Cyaniel.Models.Characters
{
    public class GameStatisticModel
    {
        /// <summary>
        /// The primary key this model is based on. This will be contextual and up to the developer
        /// to know which table this key refers to.
        /// </summary>
        public int PrimaryKey { get; set; }

        public int AdvancementFactId { get; set; }

        /// <summary>
        /// Gets or sets the name of the statistic.
        /// 
        /// For skills, this would be the name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the statistic, or its category.
        /// 
        /// For some skills this could be "Combat Skills" or for an attribute, this might be "Attributes",
        /// usually refers to the AttributeType.Name of an Attribute.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The value of the statistic.
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// Gets or sets the notes attached to this statistic model.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gates that are required to acquire this GameStatistic.
        /// 
        /// It is wise to test all gates (in that they meet the expected rank, or exist on a charactermodel)
        /// to see if this GameStatistic is available for selection.
        /// </summary>
        public List<GameStatisticModel> Gates { get; set; }

        public GameStatisticModel()
        {
            Gates = new List<GameStatisticModel>();
        }
    }
}
