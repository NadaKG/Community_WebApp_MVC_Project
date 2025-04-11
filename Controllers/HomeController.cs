using Community_With_CMS.Data.Context;
using Community_With_CMS.Models;
using Community_With_CMS.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;


namespace Community_With_CMS.Controllers
{
    public class HomeController : BaseController
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IStringLocalizer<HomeController> localizer) : base(context)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)}
                );

            return LocalRedirect(returnUrl);
        }
        public async Task<IActionResult> IndexAsync()
        {
            
            var homeViewModel = new HomeViewModel
            {
                LatestFourPosts = await _context.Post
                    .Where(p => p.IsVisible)
                    .OrderByDescending(p => p.Date)
                    .Take(4)
                    .ToListAsync()
            };

            ViewBag.MenuItems = await _context.Menu
                .OrderBy(m => m.Position)
                .Include(m => m.Categories)
                .ThenInclude(c => c.Posts)
                .ToListAsync();
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Home/PostsByCategory/{id}")]
        public async Task<IActionResult> PostsByCategory(int id)
        {
            var category = await _context.Category
                                .Include(c => c.Posts)
                                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return View(category);
        }


        [HttpGet("Home/PostDetails/{id}")]
        public async Task<IActionResult> PostDetails(int id)
        {
            var post = await _context.Post
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsVisible);

            if (post == null)
            {
                return NotFound("The requested post does not exist or is not visible.");
            }

            return View(post);
        }
    }
}
