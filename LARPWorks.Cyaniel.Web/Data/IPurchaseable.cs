using System;

namespace LARPWorks.Cyaniel.Data
{
    public interface IPurchaseable
    {
        DateTime PurchaseDate { get; set; }
        string Name { get; set; }
        int ExperienceCost { get; set; }
    }
}
