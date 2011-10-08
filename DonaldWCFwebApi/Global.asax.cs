using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DonaldWCFwebApi.APIs;
using DonaldWCFwebApi.Resources;
using Microsoft.ApplicationServer.Http;
using Microsoft.ApplicationServer.Http.Activation;

namespace DonaldWCFwebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

#if DEBUG
            HttpConfiguration configuration = new WebApiConfiguration();// {EnableTestClient = true};
            configuration.EnableTestClient = true;
            configuration.Formatters.Add(new JpgProcessor());
//            var configuration = HttpHostConfiguration
            routes.MapServiceRoute<DonaldApi>("api/donald", configuration);
//            routes.Add(new ServiceRoute("api/donald", new HttpServiceHostFactory() {Configuration = configuration}, typeof(DonaldApi)));
#else
            routes.Add(new ServiceRoute("api/donald", new HttpServiceHostFactory(), typeof(DonaldApi)));
#endif
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

    public class JpgProcessor : MediaTypeFormatter
    {
        public JpgProcessor()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/jpg"));
        }
        protected override object OnReadFromStream(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            throw new NotImplementedException();
        }

        protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext context)
        {
            var donaldEpisode = value as DonaldEpisode;
        }
    }
}