using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Singer
    {
        public int Singerid { get; set; }
        public string Name { get; set; } 
        public string Biography { get; set; }
        public string Picture { get; set; }
        public string CountSongs { get; set; }
        public string LinkToSinger { get; set; }

        public ICollection<SuiteСhord> SuiteChords{get; set;}
        public Singer ()
        {
            SuiteChords = new List<SuiteСhord>();
        }

        public Singer(string name, string picture, string countSongs, string linkToSinger)
        {
            this.Name = name;
            this.Picture = picture;
            this.CountSongs = countSongs;
            this.LinkToSinger = linkToSinger;
        }
    }
}