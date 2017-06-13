using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LARPWorks.Cyaniel.Web.Features.SharedViews;
using MySQL;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class RegisterViewModel : BaseCyanielViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public User BuildUser()
        {
            return new User
            {
                Email = Email,
                PasswordHash = SHA256(Password),
                FirstName = FirstName,
                LastName = LastName,
                Username = Username
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
