using Community_With_CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Community_With_CMS.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
