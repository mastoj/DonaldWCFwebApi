using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Web.Routing;
using DonaldWCFwebApi.APIs;
using DonaldWCFwebApi.MessageHandlers;
using DonaldWCFwebApi.Processors;
using Microsoft.ApplicationServer.Http;

namespace DonaldWCFwebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IList<UriExtensionMapping> UriExtensionMappings { get; set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            HttpConfiguration configuration = new WebApiConfiguration();// {EnableTestClient = true};
            configuration.Formatters.Add(new JpgProcessor());
            configuration.MessageHandlers.Add(typeof(UriFormatHandler));
#if DEBUG
            configuration.EnableTestClient = true;
#endif
            routes.MapServiceRoute<DonaldApi>("api/donald", configuration);
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}