using System.Collections.Generic;
using LARPWorks.Cyaniel.Models;
using Nancy.Security;

namespace LARPWorks.Cyaniel.Features.Users.Authentication
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get { return _user.Username; } }
        public IEnumerable<string> Claims { get; }


        private readonly User _user;
        public UserIdentity(User user)
        {
            _user = user;
        }

        public User GetUser()
        {
            return _user;
        }
    }
}
