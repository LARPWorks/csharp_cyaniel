using System;
using LARPWorks.Cyaniel.Web.Features.Users.Authentication;
using LARPWorks.Cyaniel.Web.Models.Factories;
using MySQL;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace LARPWorks.Cyaniel.Web.Features.Users
{
    public class MySQLUserMapper : IUserMapper
    {
        private readonly IDbFactory _dbFactory;

        public MySQLUserMapper(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            using (var db = _dbFactory.Create())
            {
                var token = db.SingleOrDefault<AuthenticationToken>("SELECT * FROM AuthenticationTokens WHERE Id=@0", identifier.ToByteArray());
                if (token == null)
                {
                    return null;
                }

                return new UserIdentity(db.SingleOrDefault<User>("SELECT * FROM Users WHERE Id=@0", token.UserId));
            }
        }
    }
}
