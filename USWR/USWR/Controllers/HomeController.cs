using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using USWR.Models;

namespace USWR.Controllers
{
    /*public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }
        /* public IDictionary dictionary;
         USWRContext db;
         public HomeController(IDictionary context)
         {
             dictionary = context;
         }
         public IActionResult Index()
         {
             ViewBag.Sites = dictionary.GetData();
             return View();
         }
         [HttpGet]
         public ActionResult Delete(Guid id)
         {
             ViewBag.Id = id;
             return View();
         }
         [HttpPost]
         public ActionResult DeleteSave(Guid id)
         {
             dictionary.Delete(id);
             ViewBag.Sites = dictionary.GetData();
             return View("Index");
         }
         [HttpGet]
         public ActionResult Update(Guid id)
         {
             ViewBag.Id = id;
             ViewBag.Sites = dictionary.GetData();
             return View();
         }

         [HttpPost]
         public ActionResult UpdateSave(Guid id, string header, string link, string keywords, string description)
         {
             dictionary.Update(id, header, link, keywords, description);
             ViewBag.Sites = dictionary.GetData();
             return View("Index");
         }
         [HttpGet]
         public ActionResult Add()
         {
             return View();
         }

         [HttpPost]
         public ActionResult AddSave(string header, string link, string keywords, string description)
         {
             dictionary.Add(header, link, keywords, description);
             ViewBag.Sites = dictionary.GetData();
             return View("Index");
         }

    }*/
    /*public class HomeController : Controller
    {
        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content($"ваша роль: {role}");
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult About()
        {
            return Content("Вход только для администратора");
        }
    }*/
}
