using Community_With_CMS.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Community_With_CMS.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ViewBag.Categories = await _context.Category.ToListAsync();

            var menusWithCategories = await _context.Menu.OrderBy(m => m.Position).Include(m => m.Categories)
                                                          .ThenInclude(c => c.Posts)
                                                          .ToListAsync();
            ViewBag.MenuItems = menusWithCategories;

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
