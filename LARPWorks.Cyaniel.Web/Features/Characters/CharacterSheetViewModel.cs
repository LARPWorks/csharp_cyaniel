using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Features.SharedViews;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Characters;
using LARPWorks.Cyaniel.Models.Factories;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterSheetViewModel : BaseCyanielViewModel
    {

        public CharacterModel Character { get; set; }

        public bool UseGuidedView { get; set; }

        public string[] Sections { get; set; }

        public GameStatisticModel[] Skills { get; set; }
        public GameStatisticModel[] Attributes { get; set; }
        public GameStatisticModel[] Esoterics { get; set; }
        public GameStatisticModel[] Perks { get; set; }
        public GameStatisticModel[] Flaws { get; set; }
        public GameStatisticModel[] Traits { get; set; }
        public GameStatisticModel[] Exoterics { get; set; }
        public GameStatisticModel[] FamilyJobAndHometownSkills { get; set; }
        public GameStatisticModel[] Cultures { get; set; }
        public GameStatisticModel[] SocialStatuses { get; set; }
        public GameStatisticModel[] MageGuilds { get; set; }
        public GameStatisticModel[] KnightOrders { get; set; }
        public GameStatisticModel[] KnightOaths { get; set; }
        public GameStatisticModel[] MageTechniques { get; set; }
        public GameStatisticModel[] Rituals { get; set; }
        public GameStatisticModel[] BasicFoci { get; set; }
        public GameStatisticModel[] AdvancedFoci { get; set; }
        public GameStatisticModel[] BasicManeuvers { get; set; }
        public GameStatisticModel[] AdvancedManeuvers { get; set; }
        public GameStatisticModel[] Concepts { get; set; }
        public GameStatisticModel[] Leaders { get; set; }
        public GameStatisticModel[] Followers { get; set; }

        public Dictionary<string, List<GameStatisticModel>> CultureSkills { get; set; }
        public Dictionary<string, List<GameStatisticModel>> SocialStatusSkills { get; set; }

        public GameStatisticModel NewGameStatisticModel { get; set; }

        public CharacterSheetViewModel()
        {
            UseGuidedView = true;
            CultureSkills = new Dictionary<string, List<GameStatisticModel>>();
            SocialStatusSkills = new Dictionary<string, List<GameStatisticModel>>();

            Sections = new []
            {
                "Character Sheet",
                "Attributes & Skills",
                //"Perks & Flaws",
                //"Foci & Languages",
                //"Maneuvers",
                //"Priest Rituals",
                //"Magic",
                //"Knight Rank & Oaths",
                //"Paladin Powers",
                //"Leadership",
                //"Biography",
                //"Notes"
            };

            Character = new CharacterModel();
            NewGameStatisticModel = new GameStatisticModel();
        }

        public string SanitizeSectionName(string sectionName)
        {
            return sectionName.Replace(" ", string.Empty).Replace("&", string.Empty);
        }

        public override void LoadFromDatabase(IDbFactory dbFactory)
        {
            using (var db = dbFactory.Create())
            {
                Attributes = db.Fetch<GameStatisticModel>(
                    "SELECT Id AS PrimaryKey, Name FROM Facts WHERE FactTypeId=@0", FactTypeEnum.Attributes).ToArray();
                Skills = LoadGameStatistics(db, AdvancementListEnum.Skills);
                Esoterics = LoadGameStatistics(db, AdvancementListEnum.Esoterics);
                Exoterics = LoadGameStatistics(db, AdvancementListEnum.Exoterics);
                Perks = LoadGameStatistics(db, AdvancementListEnum.Perks);
                Flaws = LoadGameStatistics(db, AdvancementListEnum.Flaws);
                Cultures = LoadGameStatistics(db, AdvancementListEnum.Cultures);
                SocialStatuses = LoadGameStatistics(db, AdvancementListEnum.SocialStatuses);
                FamilyJobAndHometownSkills = LoadGameStatistics(db, AdvancementListEnum.NonCombatSkills);
                MageGuilds = LoadGameStatistics(db, AdvancementListEnum.MageGuilds);
                KnightOrders = LoadGameStatistics(db, AdvancementListEnum.KnightOrders);
                KnightOaths = LoadGameStatistics(db, AdvancementListEnum.KnightOaths);
                MageTechniques = LoadGameStatistics(db, AdvancementListEnum.MageTechniques);
                Rituals = LoadGameStatistics(db, AdvancementListEnum.Rituals);
                Traits = LoadGameStatistics(db, AdvancementListEnum.Traits);
                BasicFoci = LoadGameStatistics(db, AdvancementListEnum.BasicFoci);
                AdvancedFoci = LoadGameStatistics(db, AdvancementListEnum.AdvancedFoci);
                BasicManeuvers = LoadGameStatistics(db, AdvancementListEnum.BasicManeuvers);
                AdvancedManeuvers = LoadGameStatistics(db, AdvancementListEnum.AdvancedManeuvers);
                Concepts = LoadGameStatistics(db, AdvancementListEnum.Concepts);
                Leaders = new GameStatisticModel[1] { new GameStatisticModel() { Name = "test"} };

                var cultureSkillFacts =
                    db.Fetch<AdvancementListFact>(
                        "SELECT * FROM AdvancementListFacts WHERE AdvancementListFacts.AdvancementListId=@0",
                        (int) AdvancementListEnum.CultureSkills);
                if (cultureSkillFacts.Any())
                {
                    var cultureSkillModifiers =
                        db.Fetch<AdvancementListFactModifier>(Sql.Builder
                            .Select("*")
                            .From("AdvancementListFactModifiers")
                            .Where("AdvancementListFactId IN (@ids)",
                                new {ids = cultureSkillFacts.Select(s => s.Id).ToArray()}));
                    foreach (var culture in Cultures)
                    {
                        CultureSkills.Add(culture.Name, new List<GameStatisticModel>());
                        foreach (var cultureSkill in cultureSkillFacts)
                        {
                            var modifiers =
                                cultureSkillModifiers.Where(csm => csm.AdvancementListFactId == cultureSkill.Id);
                            if (modifiers.Any(csm => csm.FactRequirementId == culture.PrimaryKey))
                            {
                                CultureSkills[culture.Name].Add(
                                    Skills.FirstOrDefault(s => s.PrimaryKey == cultureSkill.FactId));
                            }
                        }
                    }
                }

                var socialStatusSkillFacts =
                    db.Fetch<AdvancementListFact>(
                        "SELECT * FROM AdvancementListFacts WHERE AdvancementListFacts.AdvancementListId=@0",
                        (int)AdvancementListEnum.SocialStatusSkills);
                if (socialStatusSkillFacts.Any())
                {
                    var socialStatusSkillModifiers =
                        db.Fetch<AdvancementListFactModifier>(Sql.Builder
                            .Select("*")
                            .From("AdvancementListFactModifiers")
                            .Where("AdvancementListFactId IN (@ids)",
                                new {ids = socialStatusSkillFacts.Select(s => s.Id).ToArray()}));
                    foreach (var socialStatus in SocialStatuses)
                    {
                        SocialStatusSkills.Add(socialStatus.Name, new List<GameStatisticModel>());
                        foreach (var socialStatusSkill in socialStatusSkillFacts)
                        {
                            var modifiers =
                                socialStatusSkillModifiers.Where(
                                    csm => csm.AdvancementListFactId == socialStatusSkill.Id);
                            if (modifiers.Any(csm => csm.FactRequirementId == socialStatus.PrimaryKey))
                            {
                                SocialStatusSkills[socialStatus.Name].Add(
                                    Skills.FirstOrDefault(s => s.PrimaryKey == socialStatusSkill.FactId));
                            }
                        }
                    }
                }
            }
        }

        private GameStatisticModel[] LoadGameStatistics(Database database, AdvancementListEnum list)
        {
            var sql = "SELECT f.Id AS PrimaryKey,alf.Id AS AdvancementFactId,f.Name AS Name,ft.Name AS Category " +
                      "FROM AdvancementListFacts AS alf " +
                      "LEFT JOIN Facts AS f ON f.Id=alf.FactId " +
                      "LEFT JOIN FactTypes AS ft ON ft.Id=f.FactTypeId " +
                      "WHERE alf.AdvancementListId=@0";

            if (!IsAdmin())
            {
                sql += " AND alf.IsStaffOnly=0";
            }

            var models = database.Fetch<GameStatisticModel>(sql, (int) list);
            if (models.Any())
            {
                var gates = database.Fetch<GameStatisticModel>(Sql.Builder
                    .Select("alfg.RequiredFactId AS PrimaryKey," +
                            "alfg.AdvancementListFactId AS AdvancementFactId," +
                            "f.Name")
                    .From("AdvancementListFactGates AS alfg")
                    .LeftJoin("Facts AS f").On("f.Id=alfg.RequiredFactId")
                    .Where("alfg.AdvancementListFactId IN (@ids)", new
                    {
                        ids = models.Select(m => m.AdvancementFactId)
                    }));
                foreach (var model in models)
                {
                    model.Gates.AddRange(gates.Where(g => g.AdvancementFactId == model.AdvancementFactId));
                }
            }

            return models.ToArray();
        }
    }
}
