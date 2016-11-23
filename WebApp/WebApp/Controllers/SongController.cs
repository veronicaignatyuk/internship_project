using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult Song(int? id, int? ids)
        {
            var suiteChord = db.SuiteСhords.Find(ids);
            var singersSongs = db.Singers.Find(id).SuiteChords.OrderBy(s => s.SuiteСhordId).ToList();
            ViewBag.CurrentPage = singersSongs.IndexOf(suiteChord);
            int curr = singersSongs.IndexOf(suiteChord);
            ViewBag.DBId = id;
            ViewBag.NextId = singersSongs.ElementAt(curr + 1).SuiteСhordId;
            ViewBag.Last = suiteChord.Singer.SuiteChords.Count;
            return View(suiteChord);
        }
        public PartialViewResult PartialSong(int id)
        {
            return PartialView("PartialSong", db.SuiteСhords.Find(id));
        }
        [HttpPost]
        public PartialViewResult Navigation(int id, int CurrentPage)
        {
            var singer = db.Singers.Find(id).SuiteChords.OrderBy(s => s.SuiteСhordId);
            ViewBag.NextId = singer.ElementAt(CurrentPage + 1).SuiteСhordId;
            return PartialView("PartialSong", singer.ElementAt(CurrentPage));
            //return RedirectToAction("Song","Song", new { id = model.SuiteСhordId , ids = model.SingerId});
        }
        public ActionResult UpdateSong(int id)
        {
            string token = string.Join(",", db.Fingerings.Select(x => x.Name).ToList());
            @ViewBag.Fingerings = token.ToString().Split(',');
            SuiteСhord song = db.SuiteСhords.First(s => s.SuiteСhordId == id);
            return View("UpdateSong",song);
        }
        [HttpPost]
        public ActionResult UpdateSong(int? id,string text, string tokenfield)
       {
            string[] token = tokenfield.ToString().Replace("  ", string.Empty).Split(',');
             for(int i = 0; i< token.Length; i++)
            {
                token[i] = token[i].Trim();
            }
            SuiteСhord suiteСhordToUpdate = db.SuiteСhords.First(s => s.SuiteСhordId == id);
            suiteСhordToUpdate.Text = text;
            UpdateChords(token, suiteСhordToUpdate);
            db.SaveChanges();
            return RedirectToAction("Song", new { id = suiteСhordToUpdate.SingerId, ids= suiteСhordToUpdate.SuiteСhordId });
        }

        private void UpdateChords(string[] token, SuiteСhord suiteСhordToUpdate)
        {
            if (token == null)
            {
                suiteСhordToUpdate.Fingerings = new List<Fingering>();
                return;
            }

            var selectedChordsHS = new HashSet<string>(token);
            var suiteChordFingering = new HashSet<string>
                (suiteСhordToUpdate.Fingerings.Select(c => c.Name));
            foreach (var chord in db.Fingerings)
            {
                if (selectedChordsHS.Contains(chord.Name.ToString()))
                {
                    if (!suiteChordFingering.Contains(chord.Name))
                    {
                        suiteСhordToUpdate.Fingerings.Add(chord);
                    }
                }
                else
                {
                    if (suiteChordFingering.Contains(chord.Name))
                    {
                        suiteСhordToUpdate.Fingerings.Remove(chord);
                    }
                }
            }
        }

        public JsonResult GetTag(string term)
        {
            var token = "";
            if (term != "")
            token = string.Join(",", db.Fingerings.Where(f=> f.Name.Contains(term)).ToList().Select(x => x.Name).ToList());
            else token = string.Join(",", db.Fingerings.ToList().Select(x => x.Name).ToList());
            return Json(new { data = token.ToString().Split(',') }, JsonRequestBehavior.AllowGet);
        }
    }
}