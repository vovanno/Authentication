using System.Linq;
using System.Net.Http.Formatting;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.Cors;
using MultipartDataMediaFormatter;
using Newtonsoft.Json.Serialization;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute("http://localhost:4200", headers: "*", methods: "*"));
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
