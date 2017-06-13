using System;
using Nancy;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class LoginModule : NancyModule
    {
        public LoginModule()
        {
            var viewModel = new LoginViewModel();
            Get["/login"] = parameters =>
            {
                // Called when the user visits the login page or is redirected here because
                // an attempt was made to access a restricted resource. It should return
                // the view that contains the login form

                return View["Login.cshtml", viewModel];
            };

            Get["/logout"] = parameters =>
            {
                // Called when the user clicks the sign out button in the application. Should
                // perform one of the Logout actions (see below)
                return Response.AsRedirect("");
            };

            Post["/login"] = parameters =>
            {
                // Called when the user submits the contents of the login form. Should
                // validate the user based on the posted form data, and perform one of the
                // Login actions (see below)

                return Response.AsRedirect("");
            };
        }
    }
}
