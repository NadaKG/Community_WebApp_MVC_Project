using Community_With_CMS.Models;

namespace Community_With_CMS.ViewModels
{
    public class CategoryPostsViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
