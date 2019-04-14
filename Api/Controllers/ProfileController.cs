using AuthenticateBLL.DTO;
using AuthenticateBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    [RoutePrefix("Api/Profile")]
    public class ProfileController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IImageService _image;

        public ProfileController(IUserService service, IImageService image)
        {
            _userService = service;
            _image = image;
        }

        [HttpPost]
        [Route("UploadAvatar")]
        public async Task<IHttpActionResult> UploadAvatar()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Image"];
            if (postedFile == null)
                return NotFound();
            var imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray());
            imageName = (imageName + DateTime.Now.ToString("HH:m:s tt zzz") + Path.GetExtension(postedFile.FileName))
                .Replace(" ", "").Replace(":", "");
            var filePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            postedFile.SaveAs(filePath);
            var profileClaims = (ClaimsIdentity)User.Identity;
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
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<ProfileDTO> GetUsersList()
        {
            var result = _userService.GetUsersList().ToList();
            foreach (var user in result)
            {
                user.AvatarImage = "http://localhost:51312/Image/" + user.AvatarImage;
            }
            return result;
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
            var temp = await _userService.GetUserProfile(profileId);
            temp.AvatarImage = "http://localhost:51312/Image/" + temp.AvatarImage;
            return temp;
        }

        [HttpGet]
        [Route("{id}/Images")]
        public IEnumerable<ImageDTO> GetUserImages(string id)
        {
            var result = _image.GetUserImages(id).ToList();
            if (!result.Any())
                return null;
            foreach (var image in result)
            {
                image.ImageName = "http://localhost:51312/Image/" + image.ImageName;
            }
            return result;
        }
    }
}
