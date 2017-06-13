using LARPWorks.Cyaniel.Web.Features.Users;
using LARPWorks.Cyaniel.Web.Models.Factories;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using StructureMap;

namespace LARPWorks.Cyaniel.Web
{
    public class LarpworksBootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
            container.Configure(
                x =>
                {
                    x.For<IDbFactory>().Use(new CyanielDatabaseFactory()).Singleton();
                    x.For<IUserMapper>().Use<MySQLUserMapper>();
                });

            // This code exists to enable session-cookie authentication
            // for the website.
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UserMapper = container.GetInstance<IUserMapper>()
                };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            // Perform registration that should have an application lifetime
        }

        protected override void ConfigureRequestContainer(IContainer container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
        }

        protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.ViewLocationConventions.Clear();

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, viewLocationContext) =>
                    "Features/" + viewLocationContext.ModulePath + "/Views/" + viewName);

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, viewLocationContext) =>
                    "Features/Home/Views/" + viewName);

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, viewLocationContext) =>
                    "Features/" + viewName);

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, viewLocationContext) =>
                    "Features/SharedViews/" + viewName);
        }
    }
}
