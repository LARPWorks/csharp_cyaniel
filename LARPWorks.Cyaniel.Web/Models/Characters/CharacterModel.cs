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
        public List<LeaderModel> Leaders { get; protected set; }

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
            Leaders = new List<LeaderModel>();

            _backingCharacter = new Character();
            Name = "Blank Character";
            Id = -1;
            CreationDate = DateTime.UtcNow;
        }


        //public bool AddFact(IDbFactory factory, Fact fact) {
        //    using (var db = factory.Create())
        //    {
        //        db.Insert(fact);
        //        return true;
        //    }
        //}

        // Returns true if the write is successful, false otherwise
        public bool UpdateCharacterToDatabase(IDbFactory factory) {
            using (var db = factory.Create()) {
                return (db.Update(_backingCharacter) == 1); // returns true if 1 row is updated (expected), returns false otherwise
            }
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

                var attributeTypes = db.Fetch<FactType>("SELECT * FROM FactTypes").ToArray();
                var characterAttributes =
                    db.Fetch<CharacterFact>("SELECT * FROM CharacterFacts WHERE CharacterId=@0", characterId)
                    .ToArray();

                if (characterAttributes.Length == 0)
                {
                    return model;
                }
                
                var attributes =
                    db.Fetch<Fact>(Sql.Builder.Select("*")
                        .From("Facts")
                        .Where("Id IN (@ids)", characterAttributes.Select(ca => ca.FactId).Distinct().ToArray()))
                    .ToArray();

                model.Skills = LoadSkills(attributeTypes, characterAttributes, attributes);
                model.Attributes = LoadAttributes(attributeTypes, characterAttributes, attributes);
            }

            return model;
        }

        private static List<AttributeModel> LoadAttributes(FactType[] dbAttributeTypes,
            CharacterFact[] dbCharacterAttributes,
            Fact[] dbAttributes)
        {
            var attributes = new List<AttributeModel>();

            var attributeTypes = dbAttributeTypes.FirstOrDefault(at => at.Name == "Attributes");
            if (attributeTypes == null)
            {
                return attributes;
            }

            var attributeAttributes = dbAttributes.Where(a => a.FactTypeId == attributeTypes.Id).ToArray();
            var characterAttributes =
                dbCharacterAttributes.Where(ca => attributeAttributes.Any(aa => aa.Id == ca.FactId)).ToArray();
            foreach (var characterAttribute in characterAttributes)
            {
                var dbAttribute = attributeAttributes.First(aa => aa.Id == characterAttribute.FactId);
                var attribute = new AttributeModel
                {
                    Rank = characterAttribute.Rank,
                    Name = dbAttribute.Name
                };

                attributes.Add(attribute);
            }

            return attributes;
        }

        private static List<SkillModel> LoadSkills(FactType[] factTypes, CharacterFact[] characterFacts,
            Fact[] facts)
        {
            var skills = new List<SkillModel>();

            var skillTypes = factTypes.Where(at => at.Name.Contains("Skills")).ToArray();
            if (!skillTypes.Any())
            {
                return skills;
            }

            var skillAttributes = facts.Where(a => skillTypes.Any(st => st.Id == a.FactTypeId)).ToArray();
            var characterSkills =
                characterFacts.Where(ca => skillAttributes.Any(sa => sa.Id == ca.FactId)).ToArray();
            foreach (var characterSkill in characterSkills)
            {
                var attribute = skillAttributes.First(sa => sa.Id == characterSkill.FactId);
                var skill = new SkillModel
                {
                    Rank = characterSkill.Rank,
                    Name = attribute.Name,
                    Category = skillTypes.First(st => st.Id == attribute.FactTypeId).Name
                };

                skills.Add(skill);
            }

            return skills;
        }
    }
}
