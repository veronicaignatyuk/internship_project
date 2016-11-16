using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Parser;
using PagedList.Mvc;
using PagedList;

namespace WebApp.Controllers
{
    public class SingerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Singer(int? id, int? page)
        {
            Singer singer = db.Singers.Find(id);
            ViewBag.page =  (page ?? 1); 
            return View(singer);
        }

    }
}