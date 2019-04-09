using System.ComponentModel.DataAnnotations;

namespace ApplicationDAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
    }
}
