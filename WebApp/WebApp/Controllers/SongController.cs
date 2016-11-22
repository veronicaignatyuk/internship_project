﻿using Newtonsoft.Json;
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
        public ActionResult UpdateSong(SuiteСhord model)
        {
            //List<Fingering> fingerings = new List<Fingering>(); 
            //foreach (var fing in modelFing)
            //{
            //    if (!string.IsNullOrEmpty(fing.Name))
            //    {
            //fingerings.Add(db.Fingerings.First(f => f.Name == fing.Name));
            //    }
            //}
            SuiteСhord suiteСhord = db.SuiteСhords.First(s => s.SuiteСhordId==model.SuiteСhordId);
            suiteСhord.Text = model.Text;
                db.Entry(suiteСhord).State = EntityState.Modified;
                db.SaveChanges();
            return RedirectToAction("Song", new { id = suiteСhord.SingerId, ids= suiteСhord.SuiteСhordId });
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