using LARPWorks.Cyaniel.Features.Users.Authentication;
using LARPWorks.Cyaniel.Models.Factories;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using Nancy.Cryptography;
using StructureMap;

namespace LARPWorks.Cyaniel
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
            var cryptographyConfiguration = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(new PassphraseKeyGenerator("SuperSecretPass",
                    new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })),
                new DefaultHmacProvider(new PassphraseKeyGenerator("UberSuperSecure",
                    new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })));

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "/login",
                    UserMapper = container.GetInstance<IUserMapper>(),
                    CryptographyConfiguration = cryptographyConfiguration
                };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);

            // This code exists to 'hack' in a tempdata standin. This allows
            // some data to transfer between requests.
            //pipelines.BeforeRequest += (ctx) =>
            //{
            //    if (ctx.Request.Session["TempData"] != null && !string.IsNullOrEmpty(ctx.Request.Session["TempData"] as string))
            //    {
            //        ctx.ViewBag.TempData = ctx.Request.Session["TempData"];
            //        ctx.ViewBag.TempType = ctx.Request.Session["TempType"];
            //        ctx.Request.Session.DeleteAll();
            //    }
            //    return null;
            //};
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
