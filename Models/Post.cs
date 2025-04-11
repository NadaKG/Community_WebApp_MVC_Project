using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Community_With_CMS.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public bool IsVisible { get; set; }
        [Display(Name ="Upload Image")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Upload File")]

        public string? FileUrl { get; set; }
    }
}
