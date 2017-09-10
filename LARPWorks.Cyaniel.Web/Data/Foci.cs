using System;
using System.Collections.Generic;

namespace LARPWorks.Cyaniel.Data
{
    public class Foci : IPurchaseable
    {
        public string Name { get; set; }
        public int ExperienceCost { get; set; }
        public List<FociType> Types { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
