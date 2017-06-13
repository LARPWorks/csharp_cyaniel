using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LARPWorks.Cyaniel.Web.Models.Factories;
using MySQL;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class LoginModule : CyanielModule
    {
        public LoginModule(IDbFactory dbFactory) : base("Users")
        {
            Get["/login"] = parameters =>
            {
                var viewModel = GetViewModel<LoginViewModel>();
                // Called when the user visits the login page or is redirected here because
                // an attempt was made to access a restricted resource. It should return
                // the view that contains the login form

                return View["Login.cshtml", viewModel];
            };

            Get["/logout"] = parameters =>
            {
                //var viewModel = GetViewModel<LoginViewModel>();
                // Called when the user clicks the sign out button in the application. Should
                // perform one of the Logout actions (see below)
                return this.LogoutAndRedirect("/");
            };

            Post["/login"] = parameters =>
            {
                var viewModel = this.Bind<LoginViewModel>();
                // Called when the user submits the contents of the login form. Should
                // validate the user based on the posted form data, and perform one of the
                // Login actions (see below)

                var passwordHash = SHA256(viewModel.Password);
                viewModel.Password = string.Empty;

                try
                {
                    using (var db = dbFactory.Create())
                    {
                        var user = db.SingleOrDefault<User>("SELECT * FROM Users WHERE Username=@0", viewModel.Username);
                        if (user == null
                            || user.PasswordHash != passwordHash)
                        {
                            ViewBag.ValidationError = "Username or password does not match.";
                            return View["Login.cshtml", viewModel];
                        }

                        var token = db.SingleOrDefault<AuthenticationToken>("SELECT * FROM AuthenticationTokens WHERE UserId=@0",
                            user.Id);

                        if (token == null)
                        {
                            token = new AuthenticationToken
                            {
                                CreatedOn = DateTime.UtcNow,
                                UserId = user.Id,
                                Id = Guid.NewGuid().ToByteArray()
                            };
                            db.Insert(token);
                        }

                        return this.LoginAndRedirect(new Guid(token.Id));
                    }

                }
                catch (Exception e)
                {
                    ViewBag.ValidationError = e.Message;
                    return View["Login.cshtml", viewModel];
                }
            };
        }

        private static string SHA256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0,
                Encoding.UTF8.GetByteCount(password));
            return crypto.Aggregate(hash, (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}
