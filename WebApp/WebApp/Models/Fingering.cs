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
        public ICollection<SuiteСhord> SuiteСhords { get; set; }
        public Fingering()
        {
            SuiteСhords = new List<SuiteСhord>();
        }

        public Fingering(string name, string picture):this()
        {
            this.Name = name;
            this.Picture = picture;
        }

        public Fingering(string name, string picture, SuiteСhord suiteChord) : this(name, picture)
        {
            this.SuiteСhords.Add(suiteChord);
        }
    }
}