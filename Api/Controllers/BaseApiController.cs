using System.Security.Claims;
using System.Web.Http;

namespace Api.Controllers
{
    public class BaseApiController : ApiController
    {
        protected bool Secure(string id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            return identity.FindFirst("Id").Value == id || identity.FindFirst(ClaimTypes.Role).Value == "admin";
        }
    }
}
