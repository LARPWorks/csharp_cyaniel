using System;
using System.Linq;
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
            Get["/view/{CharacterId:int}/{Stage?Details}"] = parameters =>
            {
                bool useGuidedView = true;
                var model = GetViewModel<CharacterSheetViewModel>(dbFactory);

                ViewBag.Stage = parameters.Stage;

                var stagePage = "Sheets/" + ViewBag.Stage + ".cshtml";

                return
                    View[useGuidedView ? stagePage : "AdvancedView.cshtml", model];
            };

            Get["/index"] = parameters => View["Index.cshtml", BuildIndexModel()];
            Post["/index"] = parameters =>
            {
                var model = GetViewModel(this.Bind<CharacterListViewModel>());

                UInt64 newCharacterID;

                using (var db = dbFactory.Create())
                {
                    var newCharacter = new Character
                    {
                        Name = model.NewCharacterName,
                        CreationDate = DateTime.UtcNow,
                        LastUpdate = DateTime.UtcNow,
                        UserId = model.CurrentUser.Id
                    };

                    newCharacterID = (UInt64) db.Insert(newCharacter);
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
                //return View["Index.cshtml", model];
                return Response.AsRedirect("/characters/view/" + newCharacterID);
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
    }
}
