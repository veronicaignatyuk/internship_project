using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace WebApp.Models
{
    public class Lyrics
    { 
        public int LyricsId { get; set; }
        public string Line { get; set; }
        public SuiteСhord SuiteChord { get; set; }
        public ICollection<Fingering> Fingerings{ get; set; }
        public Lyrics()
        {
            Fingerings = new List<Fingering>();
        }
    }
}