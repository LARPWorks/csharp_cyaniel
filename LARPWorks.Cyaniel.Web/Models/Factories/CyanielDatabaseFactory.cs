namespace LARPWorks.Cyaniel.Models.Factories
{
    public class CyanielDatabaseFactory : IDbFactory
    {
        public Database Create()
        {
            return new Database("MySQL");
        }
    }
}
