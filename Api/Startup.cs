using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using AppContext = AuthenticationDAL.Context.AppContext;

[assembly: OwinStartup(typeof(Api.Startup))]
namespace Api
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppContext.Create);
            ConfigureOAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new OAuthAuthorizationServerProvider()
            };
            app.UseOAuthBearerTokens(oAuthServerOptions);
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }
    }
}
