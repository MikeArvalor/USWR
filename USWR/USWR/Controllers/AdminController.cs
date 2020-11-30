using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USWR.Classes;
using USWR.Models;

namespace USWR.Controllers
{
    public class AdminController : Controller
    {
        



        private USWRContext db;
        public AdminController(USWRContext context)
        {
            db = context;
        }
        //load all sites from database
        public async Task<IActionResult> Index()
        {
            return View(await db.Sites.ToListAsync());
        }
        
        //route to comments page
        public async Task<IActionResult> Comments()
        {
            var info= (from c in db.Comments
                       join s in db.Sites on c.SiteId equals s.Id
                       join u in db.Users on c.UserId equals u.Id
                       where c.UserId==u.Id && c.SiteId==s.Id
                       select new CustomComments()
                       {
                           Id=c.Id,
                           Login=u.Login,
                           Header=s.Header,
                           Link=s.Link,
                           Comment=c.Comment
                       }).ToListAsync();
            return View(await info);
        }
        //add comments by click on input form
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sites site)
        {
            Ratings ratings = new Ratings();          
            db.Sites.Add(site);
            await db.SaveChangesAsync();
            ratings.SiteId = site.Id;
            ratings.UserId = new Guid("a5187e2d-8bee-45c5-93cc-b87b603e9c9d");
            ratings.Rating = 0;
            db.Ratings.Add(ratings);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

        //edit sites
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id != null)
            {
                Sites sites = await db.Sites.FirstOrDefaultAsync(p => p.Id == id);
                if (sites != null)
                    return View(sites);
                //return View(sites);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Sites sites)
        {
            db.Sites.Update(sites);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //delete sites
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            if (id != null)
            {
                Sites sites = await db.Sites.FirstOrDefaultAsync(p => p.Id ==id);
                if (sites != null)
                    return View(sites);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != null)
            {
                Sites sites = await db.Sites.FirstOrDefaultAsync(p => p.Id == id);
                if (sites != null)
                {
                    db.Sites.Remove(sites);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        //delete comments by id
        [HttpGet]
        [ActionName("DeleteComment")]
        public async Task<IActionResult> ConfirmDeleteComment(Guid id)
        {
            if (id != null)
            {
                var info = (from c in db.Comments
                            join s in db.Sites on c.SiteId equals s.Id
                            join u in db.Users on c.UserId equals u.Id
                            where c.UserId == u.Id && c.SiteId == s.Id
                            select new CustomComments()
                            {
                                Id=c.Id,
                                Login = u.Login,
                                Header = s.Header,
                                Link = s.Link,
                                Comment = c.Comment
                            }).FirstOrDefaultAsync(p => p.Id == id);
                if (info != null)
                    return View(await info);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            if (id != null)
            {
                Comments comment = await db.Comments.FirstOrDefaultAsync(p => p.Id == id);
                if (comment != null)
                {
                    db.Comments.Remove(comment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Comments");
                }
            }
            return NotFound();
        }
        //edit sites
        public async Task<IActionResult> EditComment(Guid id)
        {
            if (id != null)
            {
                Comments comments = await db.Comments.FirstOrDefaultAsync(p => p.Id == id);
                if (comments != null)
                    return View(comments);
                //return View(sites);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditComment(Comments comments)
        {
            db.Comments.Update(comments);
            await db.SaveChangesAsync();
            return RedirectToAction("Comments");
        }
        public async Task<IActionResult> IndexFind(string keyWords)
        {
            if (!String.IsNullOrEmpty(keyWords))
            {
                var info = (from s in db.Sites
                            where EF.Functions.Like(s.Keywords, $"%{keyWords}%")
                            select new Sites()
                            {
                                Id = s.Id,
                                Header = s.Header,
                                Link = s.Link,
                                Keywords = s.Keywords,
                                Description = s.Description
                                //Rating=r.Rating
                            }).Distinct().ToListAsync();
                return View("Index", await info);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Users()
        {
            return View(await db.Users.Where(p=>p.RoleId.Equals(2) && !p.Login.Equals("test")).ToListAsync());
        }
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            Users users = db.Users.Where(p => p.Id.Equals(id)).FirstOrDefault();
                await DeleteUs(users);
                return RedirectToAction("Index");            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUs(Users users)
        {
            db.Users.Remove(users);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
