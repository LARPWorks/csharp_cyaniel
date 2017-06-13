using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LARPWorks.Cyaniel.Web.Models.Factories
{
    public class CyanielDatabaseFactory : IDbFactory
    {
        public PetaPoco.Database Create()
        {
            return new PetaPoco.Database("MySQL");
        }
    }
}
