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

        public ActionResult Singer( int? page, int id, string sortOrder)
        {
            //ViewBag.id = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; 
            //ViewBag.CurrentSort = sortOrder;
            Singer singer = db.Singers.Find(id);
            ViewBag.page =  (page ?? 1);
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        singer = singer.SuiteChords.OrderByDescending(s => s).ToList();
            //        break;
            //    default:
            //        singer = singer.OrderBy(s => s.Name).ToList();
            //        break;
            //}
            return View(singer);
        }

        public PartialViewResult ListSongs(int? page, int id)
        {
            Singer singer = db.Singers.Find(id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            return PartialView("SuiteChordList", singer.SuiteChords.ToPagedList(pageNumber, pageSize));
        }
    }
}