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
            ViewBag.suiteChordId = id;
            var suiteChords = db.SuiteСhords;
            var singersSongs = suiteChords.Find(id).Singer.SuiteChords.ToList();
            ViewBag.Count = singersSongs.Count;
            int curr = singersSongs.IndexOf(db.SuiteСhords.Find(id));
            ViewBag.DB = id;
            ViewBag.CurrentId = curr;
            ViewBag.Next = curr + 1;
            ViewBag.Prev = curr - 1;
            return View(singersSongs);
        }

        public PartialViewResult OneSong(int suiteChordId)
        {
            ViewBag.DB = suiteChordId;
            ViewBag.suiteChordId = suiteChordId;
            var suiteChord = db.SuiteСhords.Find(suiteChordId);
            var singersSongs = db.SuiteСhords.Find(suiteChordId).Singer.SuiteChords.ToList();
            int curr = singersSongs.IndexOf(db.SuiteСhords.Find(suiteChordId));
            ViewBag.Count = singersSongs.Count;
            ViewBag.Next = curr +1;
            ViewBag.Prev =curr-1;
            return PartialView("PartialSong", suiteChord);
        }

        public PartialViewResult Navigation(int suiteChordId, int idDB)
        {

            ViewBag.suiteChordId = suiteChordId;
            ViewBag.Next = suiteChordId + 1;
            ViewBag.Prev = suiteChordId - 1;
            var suiteChords = db.SuiteСhords.Find(idDB).Singer.SuiteChords.ToList();
            var sc= suiteChords.ElementAt(suiteChordId);
            return PartialView("PartialSong", suiteChords.ElementAt(suiteChordId));
        }
    }
}