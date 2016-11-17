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
        public string CountViews { get; set; }
        public string Video { get; set; }
        public string Text { get; set; }
        public string LinkToText { get; set; }
        public int? SingerId { get; set; }
        public virtual Singer Singer { get; set; }
        public virtual ICollection<Fingering> Fingerings { get; set; }
        public SuiteСhord()
        {
            Fingerings = new List<Fingering>();
        }
        public SuiteСhord(string name, string countViews, string video, string text, string linkToText,  Singer singer)
        {
            this.Name = name;
            this.CountViews = countViews;
            this.Video = video;
            this.Text = text;
            this.LinkToText = linkToText;
            this.Singer = singer;
            this.SingerId = singer.Singerid;
        }
    }
}