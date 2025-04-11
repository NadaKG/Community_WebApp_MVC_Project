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
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Community_With_CMS.Controllers;

namespace Community_With_CMS.AdminControllers

{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class PostsController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<AppUser> _userManager;

        public PostsController(IHostingEnvironment hostingEnvironment , UserManager<AppUser> userManager, ApplicationDbContext context) : base(context)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: Posts

        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Post.Include(p => p.Category).Include(p => p.User);
            return View("~/Admin/Views/Posts/Index.cshtml", await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
          
            return View("~/Admin/Views/Posts/Details.cshtml", post);
        }

        // GET: Posts/Create

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
           
            return View("~/Admin/Views/Posts/Create.cshtml");

        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Subject,Content,CategoryId,Date,UserId,IsVisible")] Post post, IFormFile imageFile, IFormFile fileFile)
        {
            
            if (ModelState.IsValid)
            {
                string currentUserId = _userManager.GetUserId(User);
                post.UserId = currentUserId;
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageFileName = Path.GetFileName(imageFile.FileName);
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/images", imageFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    post.ImageUrl = "/uploads/images/" + imageFileName;
                }

                if (fileFile != null && fileFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(fileFile.FileName);
                    var fileId = Guid.NewGuid().ToString();
                    var fileFileName = $"{fileId}{fileExtension}";
                    var encodedFileName = WebUtility.UrlEncode(fileFileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/files", fileFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileFile.CopyToAsync(stream);
                    }
                    post.FileUrl = "/uploads/files/" + encodedFileName;
                }
                var users = await _context.Users.ToListAsync();
                ViewBag.UserId = new SelectList(users, "Id", "FirstName");
                post.Date = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
     
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View("~/Admin/Views/Posts/Create.cshtml", post);
        }


        // GET: Posts/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
          
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View("~/Admin/Views/Posts/Edit.cshtml", post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Content,CategoryId,Date,UserId,IsVisible,ImageUrl,FileUrl")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", post.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View("~/Admin/Views/Posts/Edit.cshtml", post);
        }

        // GET: Posts/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
          
            return View("~/Admin/Views/Posts/Delete.cshtml", post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        
    }
}
