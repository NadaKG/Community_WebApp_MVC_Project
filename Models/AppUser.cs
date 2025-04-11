using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Community_With_CMS.Models
{
    public class AppUser : IdentityUser
    {
       
        [MaxLength(50)]
        public string FirstName { get; set; }
      
        [MaxLength(50)]
        public string LastName { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
