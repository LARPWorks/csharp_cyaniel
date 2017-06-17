/*
 * This is a file dedicated to the constants of the cyaniel application.
 */
namespace LARPWorks.Cyaniel
{
    public enum BucketTicketStatusEnum : ushort
    {
        Unclaimed = 0,
        RequiresAttention = 1,
        InProgress = 2,
        Complete = 3
    }

    public enum FactTypeEnum : ushort
    {
        Attributes = 1,
        CombatSkills = 2,
        PhysicalSkills = 3,
        ProfessionalSkills = 4,
        LifeSkills = 5,
        Perks = 6,
        Flaws = 7,
        Traits = 8,
        Cultures = 9,
        SocialClasses = 10,
        Esoterics = 11,
        Exoterics = 12,
        MageTechniques = 13,
        PaladinPowers = 14,
        BasicManeuvers = 15,
        AdvancedManeuvers = 16
    }
}
