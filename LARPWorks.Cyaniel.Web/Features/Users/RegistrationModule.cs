using System;
using LARPWorks.Cyaniel.Web.Features.SharedViews;
using LARPWorks.Cyaniel.Web.Models.Factories;
using MySQL;
using Nancy;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule(IDbFactory dbFactory) : base("Users")
        {
            var baseCyanielViewModel = new BaseCyanielViewModel();
            Get["/register"] = parameters => View["Register.cshtml", baseCyanielViewModel];
            
            Post["/register"] = parameters =>
            {
                User newUser = this.Bind<User>();
                var validationError = "";
                var failedValidation = false;

                try
                {
                    using (var db = dbFactory.Create())
                    {
                        db.Insert(newUser);
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
                    return View["Register.cshtml", baseCyanielViewModel];
                }

                return View["Register.cshtml", baseCyanielViewModel];

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