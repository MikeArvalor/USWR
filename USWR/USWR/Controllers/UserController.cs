using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using USWR.Models;

namespace USWR.Controllers
{
    public class UserController : Controller
    {
        private USWRContext db;
        public UserController(USWRContext context)
        {
            db = context;
        }
        //load all sites from database
        public async Task<IActionResult> Index()
        {
                var info = (from s in db.Sites
                            join r in db.Ratings on s.Id equals r.SiteId
                            select new CustomSites()
                            {
                                Id = s.Id,
                                Header = s.Header,
                                Link = s.Link,
                                Keywords = s.Keywords,
                                Description = s.Description,
                                PositiveRating = db.Ratings.Count(p => p.Rating.Equals(1) && p.SiteId.Equals(r.SiteId)),
                                NegativeRating = db.Ratings.Count(p => p.Rating.Equals(-1) && p.SiteId.Equals(r.SiteId))
                                //Rating=r.Rating
                            }).Distinct().ToListAsync();
                return View(await info);
           
        }
        public async Task<IActionResult> IndexFind(string keyWords)
        {
            if (!String.IsNullOrEmpty(keyWords))
            {
                var info = (from s in db.Sites
                            join r in db.Ratings on s.Id equals r.SiteId
                            where EF.Functions.Like(s.Keywords, $"%{keyWords}%")
                            select new CustomSites()
                            {
                                Id = s.Id,
                                Header = s.Header,
                                Link = s.Link,
                                Keywords = s.Keywords,
                                Description = s.Description,
                                PositiveRating = db.Ratings.Count(p => p.Rating.Equals(1) && p.SiteId.Equals(r.SiteId)),
                                NegativeRating = db.Ratings.Count(p => p.Rating.Equals(-1) && p.SiteId.Equals(r.SiteId))
                                //Rating=r.Rating
                            }).Distinct().ToListAsync();
                return View("Index",await info);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> AddPositiveRate(Guid id)
        {
            if(!db.Ratings.Any(p=>p.SiteId.Equals(id) && p.UserId.Equals(AccountController.userId)))
            {
                Ratings ratings = new Ratings();
                ratings.SiteId = id;
                ratings.UserId = AccountController.userId;
                ratings.Rating = 1;
                await AddRate(ratings);
                return RedirectToAction("Index");
            }
            else
            {
                Ratings rating = db.Ratings.Where(p => p.SiteId.Equals(id) && p.UserId.Equals(AccountController.userId)).First();
                await DeleteRate(rating);
                return RedirectToAction("Index");
            }
           
        }
        public async Task<IActionResult> AddNegativeRate(Guid id)
        {
            if (!db.Ratings.Any(p => p.SiteId.Equals(id) && p.UserId.Equals(AccountController.userId)))
            {
                Ratings ratings = new Ratings();
                ratings.SiteId = id;
                ratings.UserId = AccountController.userId;
                ratings.Rating = -1;
                await AddRate(ratings);
                return RedirectToAction("Index");
            }
            else
            {
               Ratings rating=db.Ratings.Where(p=>p.SiteId.Equals(id) && p.UserId.Equals(AccountController.userId)).First();
               await DeleteRate(rating);
               return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(Ratings ratings)
        {           
            db.Ratings.Add(ratings);
            await  db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRate(Ratings ratings)
        {
            db.Ratings.Remove(ratings);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddComment(Guid id)
        {
            if (!db.Comments.Any(p=>p.UserId.Equals(AccountController.userId) && p.SiteId.Equals(id)))
            {               
                  Sites  sites = await db.Sites.FirstOrDefaultAsync(p => p.Id.Equals(id));
                    return View(sites);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Sites sites)
        {
            Comments comments = new Comments();
            comments.SiteId = sites.Id;
            comments.UserId = AccountController.userId;
            comments.Comment = sites.Description;
            db.Comments.Add(comments); 
            await db.SaveChangesAsync();      
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Comments(Guid id)
        {
            var info = (from c in db.Comments
                        join s in db.Sites on c.SiteId equals s.Id
                        join u in db.Users on c.UserId equals u.Id
                        where c.UserId == u.Id && c.SiteId == id
                        select new CustomComments()
                        {
                            Id = c.Id,
                            Login = u.Login,
                            Header = s.Header,
                            Link = s.Link,
                            Comment = c.Comment
                        }).ToListAsync();
            return View(await info);
        }
        public async Task<IActionResult> UserComments()
        {
            var info = (from c in db.Comments
                        join s in db.Sites on c.SiteId equals s.Id
                        join u in db.Users on c.UserId equals u.Id
                        where c.UserId == AccountController.userId
                        select new CustomComments()
                        {
                            Id = c.Id,
                            Login = u.Login,
                            Header = s.Header,
                            Link = s.Link,
                            Comment = c.Comment
                        }).ToListAsync();
            return View(await info);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            await DeleteComment(id);
            return RedirectToAction("UserComments");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
                Comments comment = await db.Comments.FirstOrDefaultAsync(p => p.Id == id);
                    db.Comments.Remove(comment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("UserComments");               
        }

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
            return RedirectToAction("Index");
        }
      
    }
}
