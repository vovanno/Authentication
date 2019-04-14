using AuthenticateBLL.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for super users with extra rights for managing other users.
    /// </summary>
    [Authorize(Roles = "admin")]
    [RoutePrefix("Api/Admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminService _adminService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IImageService imageService, IUserService userService)
        {
            _adminService = adminService;
            _imageService = imageService;
            _userService = userService;
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IHttpActionResult> DeleteUser()
        {
            var userId = HttpContext.Current.Request.Params["UserId"];
            var userProfile = await _userService.GetUserProfile(userId);
            var user = await _userService.GetUserByIdAsync(userId);
            var deleteProfile = await _adminService.DeleteProfile(userProfile);
            var deleteUser = await _adminService.DeleteAsync(user);
            if (deleteProfile && deleteUser)
                return Ok();
            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<IHttpActionResult> DeleteImage()
        {

            var arr = HttpContext.Current.Request.Params["ImageName"].Split('/');
            var imageName = arr[4];
            var imagePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            var result = await _imageService.DeleteImage(imageName);
            if (result)
                return Ok();
            return NotFound();

        }
    }
}
