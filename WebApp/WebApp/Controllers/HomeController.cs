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

        public ActionResult Index(int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var singers = db.Singers.ToList();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CountSongsSortParm = sortOrder == "songs" ? "songs_desc" : "songs";
            ViewBag.CountViewsSortParm = sortOrder == "views" ? "views_desc" : "views";
            switch (sortOrder)
                {
                    case "name_desc":
                        singers = singers.OrderByDescending(s => s.Name).ToList();
                        break;
                    case "songs_desc":
                        singers = singers.OrderByDescending(s => s.CountSongs).ToList();
                        break;
                    case "songs":
                        singers = singers.OrderBy(s => s.CountSongs).ToList();
                        break;
                    case "views_desc":
                        singers = singers.OrderByDescending(s => s.CountViews).ToList();
                        break;
                    case "views":
                        singers = singers.OrderBy(s => s.CountViews).ToList();
                        break;
                    default:
                        singers = singers.OrderBy(s => s.Name).ToList();
                        break;
                }
            return View(singers.ToList().ToPagedList(pageNumber, pageSize));
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