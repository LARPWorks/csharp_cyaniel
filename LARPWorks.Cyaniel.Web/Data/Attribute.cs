using System.Collections.Generic;

namespace LARPWorks.Cyaniel.Data
{
    public class Attribute
    {
        public string Name { get; set; }
        public int Value
        {
            get
            {
                return Ranks.Count;
            }
        }

        public List<Rank> Ranks { get; set; }

        public Attribute()
        {
            Ranks = new List<Rank>();
        }
    }
}
