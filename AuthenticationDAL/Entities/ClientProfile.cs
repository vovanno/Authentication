using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationDAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public virtual AppUser ApplicationUser { get; set; }
    }
}
