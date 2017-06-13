using System;
using LARPWorks.Cyaniel.Web.Models.Factories;
using MySQL;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Web.Features.Users
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
                 
                //if (string.IsNullOrEmpty(newUser.Username))
                //{
                //    failedValidation = true;
                //    validationError += string.Format("Must provide a username!<br>");
                //}

                if (failedValidation)
                {
                    ViewBag.ValidationError = validationError;
                    return View["Register.cshtml", viewModel];
                }

                return this.LoginAndRedirect(new Guid(token.Id));
                //return Response.AsRedirect("/");

                //var accountName = Request.Form.AccountName;
                //var password = Request.Form.Password;
                //var password2 = Request.Form.Password2;

                //if (password != password2) {

                //}
                //return View["Register.cshtml", baseCyanielViewModel];
            };
        }
    }
}