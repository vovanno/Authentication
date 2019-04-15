using System.Net;
using AuthenticateBLL.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for super users with extra rights for managing other users.
    /// </summary>
    [Authorize(Roles = "admin")]
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpDelete]
        [Route("User/{id}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var userProfile = await _userService.GetUserProfile(id);
            var user = await _userService.GetUserByIdAsync(id);
            var deleteProfile = await _adminService.DeleteProfile(userProfile);
            var deleteUser = await _adminService.DeleteAsync(user);
            if (deleteProfile && deleteUser)
                return StatusCode(HttpStatusCode.NoContent);
            return NotFound();
        }
    }
}
