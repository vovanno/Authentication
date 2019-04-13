using AuthenticateBLL.Interfaces;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{

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
            var UserProfile = await _userService.GetUserProfile(userId);
            var User = await _userService.GetUserByIdAsync(userId);
            var deleteProfile = await _adminService.DeleteProfile(UserProfile);
            var deleteUser = await _adminService.DeleteAsync(User);
            if (deleteProfile && deleteUser)
                return Ok();
            return NotFound();
        }

        [HttpDelete]
        [Route("DeleteImage")]
        public void DeleteImage()
        {
            RedirectToRoute("Api/Images/DeleteImage", null);
            //var arr = HttpContext.Current.Request.Params["ImageName"].Split('/');
            //var imageName = arr[4];
            //var imagePath = HttpContext.Current.Server.MapPath("~/Image") + "/" + imageName;
            //if (File.Exists(imagePath))
            //    File.Delete(imagePath);
            //var result = await _imageService.DeleteImage(imageName);
            //if (result)
            //    return Ok();
            //return NotFound();

        }
    }
}
