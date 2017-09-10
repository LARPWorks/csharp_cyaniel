namespace LARPWorks.Cyaniel.Data.Downtime
{
    public class DowntimeAction
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int OwnerUserId { get; set; }
        public int CharacterId { get; set; }
        public bool FromRetainer { get; set; }
        /// <summary>
        /// Gets or sets the assigned to character identifier.
        /// 
        /// If this is null, the action is currently "floating" for CharacterId
        /// to assign. This is usually for retainers.
        /// </summary>
        /// <value>
        /// The assigned to character identifier.
        /// </value>
        public int? AssignedToCharacterId { get; set; }
        public int? AssignedtoUserId { get; set; }
    }
}
