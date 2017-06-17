﻿using System;
using System.Linq;
using System.Net.Sockets;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Characters;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy;
using Nancy.ModelBinding;
using Attribute = LARPWorks.Cyaniel.Models.Attribute;

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
                var attributeTypes = db.Fetch<AttributeType>("SELECT * FROM AttributeTypes").ToArray();
                var skillAttributeTypes = attributeTypes.Where(at => at.Name.Contains("Skills")).ToArray();
                var attributeAttributeType = attributeTypes.FirstOrDefault(at => at.Name == "Attributes");

                var skillAttributes =
                    db.Fetch<Attribute>(Sql.Builder.Select("*")
                        .From("Attributes")
                        .Where("AttributeTypeId IN (@ids)", new { ids =  skillAttributeTypes.Select(ca => ca.Id).Distinct().ToArray() }))
                    .ToArray();
                model.Skills = skillAttributes.Select(sa => sa.Name).ToArray();

                var attributeAttributes =
                    db.Fetch<Attribute>(Sql.Builder.Select("*")
                        .From("Attributes")
                        .Where("AttributeTypeId=@id", new {id = attributeAttributeType.Id}))
                        .ToArray();
                model.Attributes = attributeAttributes.Select(sa => sa.Name).ToArray();
            }

            return model;
        }
    }
}
