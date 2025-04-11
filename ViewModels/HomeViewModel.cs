using Community_With_CMS.Models;

namespace Community_With_CMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Post> LatestFourPosts { get; set; }
    }
}
