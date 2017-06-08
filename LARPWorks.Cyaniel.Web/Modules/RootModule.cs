using Nancy;

namespace LARPWorks.Cyaniel.Web.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = parameters => "Hello World";
        }
    }
}
