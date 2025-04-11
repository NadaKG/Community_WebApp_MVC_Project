using Community_With_CMS.Controllers;
using Community_With_CMS.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Community_With_CMS.Admin.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {

        public AdminController(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult Dashboard()
        {
            return View("~/Admin/Views/Dashboard/Dashboard.cshtml");
        }

    }
}
