using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    //[Authorize]
    [RoutePrefix("Api/Profile")]
    public class ProfileController : ApiController
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService service)
        {
            _userService = service;
        }

        //[HttpPost]
        //[Route("UploadAvatar")]
        //public IHttpActionResult UploadImage()
        //{

        //}

        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<ProfileDTO> GetUsersList()
        {
            return _userService.GetUsersList();
        }

        [HttpPost]
        [Route("ModifyProfile")]
        public async Task<IHttpActionResult> ModifyProfileAsync(ProfileDTO profile)
        {
            var profileClaims = (ClaimsIdentity)User.Identity;
            profile.Id = profileClaims.FindFirst("Id").Value;
            var result = await _userService.ModifyUserProfile(profile);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<ProfileDTO> GetUserProfile()
        {
            var profileId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
            return await _userService.GetUserProfile(profileId);
        }
    }
}
