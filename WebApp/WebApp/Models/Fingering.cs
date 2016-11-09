using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Fingering
    {
        public int FingeringId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public ICollection<Lyrics> ListOfLyrics { get; set; }
        public Fingering()
        {
            ListOfLyrics = new List<Lyrics>();
        }
    }
}