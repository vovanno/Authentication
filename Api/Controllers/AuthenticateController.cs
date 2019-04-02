using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;


namespace Api.Controllers
{
    [Route("Api/Authentication/")]
    public class AuthenticateController : ApiController
    {
        private readonly IAuthenticateService _authenticate;
        
        public AuthenticateController(IAuthenticateService service)
        {
            _authenticate = service;
        }

        public async Task SetInitialData()
        {
            var admin= new UserDTO() {Email = "admin@gmail.com",UserName = "Admin"};
            await _authenticate.SetInitialData(admin);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Api/Authentication/Register")]
        public async Task<IHttpActionResult> Register(UserDTO model)
        {
            await SetInitialData();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticate.Create(model);
            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded)
                return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            if (ModelState.IsValid)
            {
                // Ошибки ModelState для отправки отсутствуют, поэтому просто возвращается пустой BadRequest.
                return BadRequest();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize]
        [Route("api/GetUserClaims")]
        public UserDTO GetUserClaims()
        {

            var identityClaims = (ClaimsIdentity)User.Identity;
            var model = new UserDTO()
            {
                Id = identityClaims.FindFirst("Id").Value,
                UserName = identityClaims.FindFirst("UserName").Value,
                Email = identityClaims.FindFirst("Email").Value,
            };
            return model;
        }
    }
}
