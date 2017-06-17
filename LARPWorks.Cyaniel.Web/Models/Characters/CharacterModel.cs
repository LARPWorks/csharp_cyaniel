using System;
using System.Collections.Generic;
using System.Linq;
using LARPWorks.Cyaniel.Models.Factories;

namespace LARPWorks.Cyaniel.Models.Characters
{
    public class CharacterModel
    {
        public int Id { get { return _backingCharacter.Id; } set { _backingCharacter.Id = value; } }
        public string Name { get { return _backingCharacter.Name; } set { _backingCharacter.Name = value; } }
        public List<SkillModel> Skills { get; protected set; }
        public List<AttributeModel> Attributes { get; protected set; }

        public DateTime CreationDate
        {
            get { return _backingCharacter.CreationDate; }
            set { _backingCharacter.CreationDate = value; }
        }

        public int ExperienceTotal { get; set; }

        private Character _backingCharacter;

        public CharacterModel()
        {
            Skills = new List<SkillModel>();
            Attributes = new List<AttributeModel>();

            _backingCharacter = new Character();
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

                model._backingCharacter = character;

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
                model.Attributes = LoadAttributes(attributeTypes, characterAttributes, attributes);
            }

            return model;
        }

        private static List<AttributeModel> LoadAttributes(AttributeType[] dbAttributeTypes,
            CharacterAttribute[] dbCharacterAttributes,
            Attribute[] dbAttributes)
        {
            var attributes = new List<AttributeModel>();

            var attributeTypes = dbAttributeTypes.FirstOrDefault(at => at.Name == "Attributes");
            if (attributeTypes == null)
            {
                return attributes;
            }

            var attributeAttributes = dbAttributes.Where(a => a.AttributeTypeId == attributeTypes.Id).ToArray();
            var characterAttributes =
                dbCharacterAttributes.Where(ca => attributeAttributes.Any(aa => aa.Id == ca.AttributeId)).ToArray();
            foreach (var characterAttribute in characterAttributes)
            {
                var dbAttribute = attributeAttributes.First(aa => aa.Id == characterAttribute.AttributeId);
                var attribute = new AttributeModel
                {
                    Rank = characterAttribute.Rank,
                    Name = dbAttribute.Name
                };

                attributes.Add(attribute);
            }

            return attributes;
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
                    Name = attribute.Name,
                    Category = skillTypes.First(st => st.Id == attribute.AttributeTypeId).Name
                };

                skills.Add(skill);
            }

            return skills;
        }
    }
}
