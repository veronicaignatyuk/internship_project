using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Parser;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Singers.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Singer(int id)
        {
            return View(db.Singers.Find(id));
        }
        [HttpGet]
        public ActionResult UpdateTop()
        {
           TopSingers.GetSingers("http://amdm.ru/chords/") ;
            return Redirect("~/Home/Index");
        }
    }
}