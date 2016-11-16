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
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            int pageSize =10;
            int pageNumber = (page ?? 1);
            return View(db.Singers.ToList().ToPagedList(pageNumber, pageSize));
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
        public ActionResult UpdateTop()
        {
           TopSingers.GetSingers("http://amdm.ru/chords/") ;
            return Redirect("~/Home/Index");
        }
    }
}