using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for interaction with user's accounts. Allows only authorize users.
    /// </summary>
    [Authorize]
    [RoutePrefix("Profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IImageService _image;

        public ProfileController(IUserService service, IImageService image)
        {
            _userService = service;
            _image = image;
        }

        [HttpPut]
        [Route("{id}/Avatar")]
        public async Task<IHttpActionResult> UploadAvatar(string id)
        {
            if (!Secure(id))
                return Unauthorized();
            var profileClaims = (ClaimsIdentity)User.Identity;
            if (profileClaims.FindFirst("Id").Value != id)
            {
                return Unauthorized();
            }
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Image"];
            if (postedFile == null)
                return StatusCode(HttpStatusCode.NoContent);
            var imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray());
            imageName = (imageName + DateTime.Now.ToString("HH:m:s tt zzz") + Path.GetExtension(postedFile.FileName))
                .Replace(" ", "").Replace(":", "");
            var filePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            postedFile.SaveAs(filePath);
            var image = new ImageDTO()
            {
                UserId = profileClaims.FindFirst("Id").Value,
                ImageName = imageName
            };
            var oldImage = (await _userService.GetUserProfile(image.UserId)).AvatarImage;
            if (oldImage != "" && oldImage !="DefaultUser.jpg")
            {
                var oldImagePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + oldImage;
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }
            var profileUpdate = await _userService.UpdateAvatar(image.UserId, image.ImageName);
            if (profileUpdate)
                return Ok();
            return StatusCode(HttpStatusCode.Conflict);
        }

        [HttpGet]
        [Route("~/Users")]
        public IHttpActionResult GetUsersList()
        {
            var result = _userService.GetUsersList().ToList();
            foreach (var user in result)
            {
                user.AvatarImage = "http://localhost:51312/Image/" + user.AvatarImage;
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> ModifyProfileAsync(string id, ProfileDTO profile)
        {
            if (!Secure(id))
                return Unauthorized();
            var profileClaims = (ClaimsIdentity)User.Identity;
            if (profileClaims.FindFirst("Id").Value != id)
            {
                return Unauthorized();
            }
            profile.Id = profileClaims.FindFirst("Id").Value;
            var result = await _userService.ModifyUserProfile(profile);
            if (result)
            {
                return Ok();
            }
            return StatusCode(HttpStatusCode.Conflict);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUserProfile(string id)
        {
            if (!Secure(id))
                return Unauthorized();
            var temp = await _userService.GetUserProfile(id);
            temp.AvatarImage = "http://localhost:51312/Image/" + temp.AvatarImage;
            return Ok(temp);
        }

        [HttpGet]
        [Route("{id}/Images")]
        public IHttpActionResult GetUserImages(string id)
        {
            var result =  _image.GetUserImages(id).ToList();
            if (!result.Any())
                return Ok(result);
            foreach (var image in result)
            {
                image.ImageName = "http://localhost:51312/Image/" + image.ImageName;
            }
            return Ok(result);
        }
    }
}
