using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationDAL.Entities
{
    public class ClientProfile
    {
        [Key]
        //[ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        

        //public AppUser ApplicationUser { get; set; }
    }
}
