using System;
using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Models.Factories;

namespace LARPWorks.Cyaniel.Models.Characters
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SkillModel> Skills { get; protected set; }
        public DateTime CreationDate { get; }
        public int ExperienceTotal { get; set; }

        public CharacterModel()
        {
            Name = "Blank Character";
            Id = -1;
            CreationDate = DateTime.UtcNow;
        }
        
        public static CharacterModel BuildFromDatabase(IDbFactory factory, int characterId)
        {
            var model = new CharacterModel();

            using (var db = factory.Create())
            {
                var character = db.SingleOrDefault<Character>("SELECT * FROM Characters WHERE Id=@0", characterId);

                // Quick guard clause in case character not found.
                if (character == null)
                {
                    return null;
                }

                var attributeTypes = db.Fetch<AttributeType>("SELECT * FROM AttributeTypes").ToArray();
                var characterAttributes =
                    db.Fetch<CharacterAttribute>("SELECT * FROM CharacterAttributes WHERE CharacterId=@0", characterId)
                    .ToArray();

                if (characterAttributes.Length == 0)
                {
                    return model;
                }
                
                var attributes =
                    db.Fetch<Attribute>(Sql.Builder.Select("*")
                        .From("Attributes")
                        .Where("Id IN (@ids)", characterAttributes.Select(ca => ca.AttributeId).Distinct().ToArray()))
                    .ToArray();

                model.Skills = LoadSkills(attributeTypes, characterAttributes, attributes);
            }

            return model;
        }

        private static List<SkillModel> LoadSkills(AttributeType[] attributeTypes, CharacterAttribute[] characterAttributes,
            Attribute[] attributes)
        {
            var skills = new List<SkillModel>();

            var skillTypes = attributeTypes.Where(at => at.Name.Contains("Skills")).ToArray();
            if (!skillTypes.Any())
            {
                return skills;
            }

            var skillAttributes = attributes.Where(a => skillTypes.Any(st => st.Id == a.AttributeTypeId)).ToArray();
            var characterSkills =
                characterAttributes.Where(ca => skillAttributes.Any(sa => sa.Id == ca.AttributeId)).ToArray();
            foreach (var characterSkill in characterSkills)
            {
                var attribute = skillAttributes.First(sa => sa.Id == characterSkill.AttributeId);
                var skill = new SkillModel
                {
                    Rank = characterSkill.Rank,
                    Name = attribute.Name
                };

                skills.Add(skill);
            }

            return skills;
        }
    }
}
