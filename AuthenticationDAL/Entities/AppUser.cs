using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationDAL.Entities
{
    public class AppUser : IdentityUser
    {
        public ClientProfile ClientProfile;
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }

        //public async Task<ClaimsIdentity> GenerateUserProfileAsync(UserManager<AppUser> manager, string authenticationType)
        //{
        //    var user = await manager.FindByEmailAsync(Email);
        //    var userIdentity = await manager.AddClaimAsync(user.Id,new Claim())
        //}
    }
}
