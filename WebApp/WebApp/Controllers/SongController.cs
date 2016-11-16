using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Parser;

namespace WebApp.Controllers
{
    public class SongController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Song(int? id)
        {
            SuiteСhord suiteChord = db.SuiteСhords.Find(id);
            return View(suiteChord);
        }

    }
}