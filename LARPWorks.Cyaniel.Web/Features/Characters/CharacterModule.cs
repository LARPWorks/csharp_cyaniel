using System;
using System.Linq;
using System.Net.Sockets;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Characters;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Features.Characters
{
    public class CharacterModule : CyanielModule
    {
        private readonly IDbFactory _dbFactory;

        public CharacterModule(IDbFactory dbFactory) : base("Characters")
        {
            _dbFactory = dbFactory;
            Get["/view"] = parameters => View["View.cshtml", BuildSheetModel()];
            Get["/index"] = parameters => View["Index.cshtml", BuildIndexModel()];
            Post["/index"] = parameters =>
            {
                var model = GetViewModel(this.Bind<CharacterListViewModel>());
                
                using (var db = dbFactory.Create())
                {
                    var newCharacter = new Character
                    {
                        Name = model.NewCharacterName,
                        CreationDate = DateTime.UtcNow,
                        LastUpdate = DateTime.UtcNow,
                        UserId = model.CurrentUser.Id
                    };

                    db.Insert(newCharacter);
                }

                using (var db = _dbFactory.Create())
                {
                    var characters = db.Fetch<Character>("SELECT * FROM Characters WHERE IsDeleted=0");
                    foreach (var character in characters)
                    {
                        model.Characters.Add(CharacterModel.BuildFromDatabase(_dbFactory, character.Id));
                    }
                }

                model.NewCharacterName = string.Empty;
                return View["Index.cshtml", model];
            };
            Post["/delete/{characterId}"] = parameters =>
            {
                var characterId = Int32.Parse(parameters.characterId);
                using (var db = dbFactory.Create())
                {
                    var character = db.SingleOrDefault<Character>("SELECT * FROM Characters WHERE Id=@0", characterId);
                    character.IsDeleted = true;

                    db.Save(character);
                }

                return Response.AsRedirect("/characters/index");
            };
        }

        public CharacterListViewModel BuildIndexModel()
        {
            var model = GetViewModel<CharacterListViewModel>();

            using (var db = _dbFactory.Create())
            {
                var characters = db.Fetch<Character>("SELECT * FROM Characters WHERE IsDeleted=0");
                foreach (var character in characters)
                {
                    model.Characters.Add(CharacterModel.BuildFromDatabase(_dbFactory, character.Id));
                }
            }

            return model;
        }

        public CharacterSheetViewModel BuildSheetModel()
        {
            var model = GetViewModel<CharacterSheetViewModel>();
            using (var db = _dbFactory.Create())
            {
                var factTypes = db.Fetch<FactType>("SELECT * FROM FactTypes").ToArray();
                var skillFactTypes = factTypes.Where(at => at.Name.Contains("Skills")).ToArray();

                var skillAttributes =
                    db.Fetch<Fact>(Sql.Builder.Select("*")
                        .From("Facts")
                        .Where("FactTypeId IN (@ids)", new { ids =  skillFactTypes.Select(ca => ca.Id).Distinct().ToArray() }))
                    .ToArray();
                model.Skills = skillAttributes.Select(sa => new GameStatisticModel { Name = sa.Name,
                    Category = sa.FactTypeId.ToString() }).ToArray();

                // Reconciling the category with our cached list of skill types.
                foreach (var skill in model.Skills)
                {
                    var skillAttributeType = skillFactTypes.First(at => at.Id.ToString() == skill.Category);
                    skill.Category = skillAttributeType.Name;
                }

                var attributeFacts =
                    db.Fetch<Fact>(Sql.Builder.Select("*")
                        .From("Facts")
                        .Where("FactTypeId=@id", new {id = FactTypeEnum.Attributes}))
                        .ToArray();
                model.Attributes = attributeFacts.Select(sa => 
                    new GameStatisticModel {Name = sa.Name, Category = "Attributes"}).ToArray();

                var perkFacts =
                    db.Fetch<Fact>("SELECT * FROM Facts WHERE FactTypeId=@0", FactTypeEnum.Perks);
                var flawFacts =
                    db.Fetch<Fact>("SELECT * From Facts WHERE FactTypeId=@0", FactTypeEnum.Flaws);

                model.Perks =
                    perkFacts.Select(f => new GameStatisticModel {Name = f.Name, Category = "Perks"}).ToArray();
                model.Flaws =
                    flawFacts.Select(f => new GameStatisticModel {Name = f.Name, Category = "Flaws"}).ToArray();

            }

            return model;
        }
    }
}
