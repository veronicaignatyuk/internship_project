using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Parser;

namespace WebApp.Controllers
{
    public class SingerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Singer(int? id)
        {
            return View(db.Singers.Find(id));
        }

    }
}