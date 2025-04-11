using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Community_With_CMS.Data.Context;
using Community_With_CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Community_With_CMS.Controllers;

namespace Community_With_CMS.Admin.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class MenusController : BaseController
    {
      
        public MenusController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
           
            return View("~/Admin/Views/Menus/Index.cshtml", await _context.Menu.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
           
            return View("~/Admin/Views/Menus/Details.cshtml",menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
          
            return View("~/Admin/Views/Menus/Create.cshtml");
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,Position")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View("~/Admin/Views/Menus/Create.cshtml",menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
           
            return View("~/Admin/Views/Menus/Edit.cshtml", menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,Position")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            var menuToUpdate = await _context.Menu.FindAsync(id);
            if (menuToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Directly modify the properties of the retrieved object
                    menuToUpdate.Title = menu.Title;
                    menuToUpdate.Url = menu.Url;
                    menuToUpdate.Position = menu.Position;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View("~/Admin/Views/Menus/Edit.cshtml", menuToUpdate);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View("~/Admin/Views/Menus/Delete.cshtml", menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }
    }
}
