namespace LARPWorks.Cyaniel.Models.Characters
{
    public class GameStatisticModel
    {
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
        public int Rank { get; set; }
        /// <summary>
        /// Gets or sets the notes attached to this statistic model.
        /// </summary>
        public string Notes { get; set; }
    }
}
