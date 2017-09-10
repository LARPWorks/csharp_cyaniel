using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARPWorks.Cyaniel.Data
{
    public class Skill
    {
        public string Name { get; set; }
        public SkillType Type { get; set; }
        public int Value
        {
            get
            {
                return Ranks.Count;
            }
        }
        public List<Rank> Ranks { get; set; }

        public Skill()
        {
            Ranks = new List<Rank>();
        }
    }
}
