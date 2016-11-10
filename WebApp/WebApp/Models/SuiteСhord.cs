using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class SuiteСhord
    {
        public int SuiteСhordId { get; set; }
        public string Name { get; set; }
        public int CountViews { get; set; }
        public string Video { get; set; }
        public string Text { get; set; }
        public int? SingerId { get; set; }
        public Singer Singer { get; set; }
        public ICollection<Fingering> Fingerings { get; set; }
        public SuiteСhord()
        {
            Fingerings = new List<Fingering>();
        }
    }
}