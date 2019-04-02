using System.ComponentModel.DataAnnotations;

namespace AuthenticationDAL.Entities
{
    public class Images
    {
        [Key]
        public string ImageId { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
    }
}
