using LARPWorks.Cyaniel.Web.Features.SharedViews;
using MySQL;
using Nancy;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule() : base("Users")
        {
            var baseCyanielViewModel = new BaseCyanielViewModel();
            Get["/register"] = parameters => View["Register.cshtml", baseCyanielViewModel];

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            Post["/register", true] = async (x, ct) =>
            {
                User newUser = this.Bind<User>();
                var validationError = "";
                var failedValidation = false;

                if (string.IsNullOrEmpty(newUser.Username))
                {
                    failedValidation = true;
                    validationError += string.Format("Must provide a username!<br>");
                }

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
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        }
    }
}