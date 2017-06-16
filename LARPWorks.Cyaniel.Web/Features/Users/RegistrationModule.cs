using System;
using LARPWorks.Cyaniel.Models;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Features.Users
{
    public class RegistrationModule : CyanielModule
    {
        public RegistrationModule(IDbFactory dbFactory) : base("Users")
        {
            var viewModel = new RegisterViewModel();
            Get["/register"] = parameters => View["Register.cshtml", viewModel];
            
            Post["/register"] = parameters =>
            {
                viewModel = this.Bind<RegisterViewModel>();
                var user = viewModel.BuildUser();
                var token = new AuthenticationToken
                {
                    Id = Guid.NewGuid().ToByteArray(),
                    CreatedOn = DateTime.UtcNow
                };
                var validationError = "";
                var failedValidation = false;

                if (viewModel.Password != viewModel.Password2)
                {
                    ViewBag.ValidationError = "Passwords did not match.";
                    return View["Register.cshtml", viewModel];
                }

                try
                {
                    using (var db = dbFactory.Create())
                    {
                        var id = db.Insert(user);
                        token.UserId = int.Parse(id.ToString());
                        db.Insert(token);
                    }
                }
                catch (Exception e)
                {
                    failedValidation = true;
                    validationError += e.Message;
                }

                if (failedValidation)
                {
                    ViewBag.ValidationError = validationError;
                    return View["Register.cshtml", viewModel];
                }

                return this.LoginAndRedirect(new Guid(token.Id));
            };
        }
    }
}