using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationDAL.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
