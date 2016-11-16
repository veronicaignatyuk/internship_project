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
            suiteChord.Singer = db.Singers.Find(suiteChord.SingerId);
            suiteChord.Fingerings = db.Fingerings.Where(p => p.SuiteСhords.Any(c => c.SuiteСhordId==suiteChord.SuiteСhordId)).ToList();
            //suiteChord.Fingerings= db.Fingerings.Where(p=> p.SuiteСhords.Select(c => c.SuiteСhordId).Contains(suiteChord.SuiteСhordId)).ToList();
            return View(suiteChord);
        }

    }
}