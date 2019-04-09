using System.ComponentModel.DataAnnotations;

namespace ApplicationDAL.Entities
{
    public class ClientProfile
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AvatarImage { get; set; }
    }
}
