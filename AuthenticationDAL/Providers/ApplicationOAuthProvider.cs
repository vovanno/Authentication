using AuthenticationDAL.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationDAL.Providers
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of OAuthAuthorizationServerProvider class, used by server to communicate with web application.
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Method responsible for validating the resource owner credentials and creating claims if user exist.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<AppUserManager>();
            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            var roleManager = userManager.GetRoles(user.Id);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Id", user.Id));
            identity.AddClaim(new Claim("Email", user.Email));
            identity.AddClaim(new Claim("UserName", user.UserName));
            foreach (var role in roleManager)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            context.Validated(identity);
        }
        /// <inheritdoc />
        /// <summary>
        /// Method responsible for validating the client.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }   
    }
}
