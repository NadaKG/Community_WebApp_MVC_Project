using System.ComponentModel.DataAnnotations;

namespace Community_With_CMS.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public ICollection<Post>? Posts { get; set; }

    }
}
