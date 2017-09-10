using System;
using System.Collections.Generic;
using LARPWorks.Cyaniel.Data.Downtime;

namespace LARPWorks.Cyaniel.Data
{
    public class Character
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        // Skills
        public List<Skill> Skills { get; set; }
        // Attributes
        public List<Attribute> Attributes { get; set; }
        // Perks/Flaws/Traits
        public List<Trait> Traits { get; set; }
        // All foci type.
        public List<Foci> Foci { get; set; }

        public LeadershipNode LeadershipGraph { get; set; }

        public Character()
        {
            CreationDate = DateTime.UtcNow;
            Skills = new List<Skill>();
            Attributes = new List<Attribute>();
            Traits = new List<Trait>();
            Foci = new List<Foci>();
        }
    }
}
