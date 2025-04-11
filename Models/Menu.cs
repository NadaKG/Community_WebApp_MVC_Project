using System.ComponentModel.DataAnnotations;

namespace Community_With_CMS.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int Position { get; set; }
        public ICollection<Category>? Categories { get; set; }

    }
}
