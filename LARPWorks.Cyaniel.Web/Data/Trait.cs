using System;

namespace LARPWorks.Cyaniel.Data
{
    public class Trait : IPurchaseable
    {
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ExperienceCost { get; set; }
        public TraitType Type { get; set; }
        public string Notes { get; set; }
    }
}
