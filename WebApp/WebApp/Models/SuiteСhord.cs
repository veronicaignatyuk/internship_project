using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [JsonObject]
    public class SuiteСhord
    {
        [JsonProperty("suiteСhordId")]
        public int SuiteСhordId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("сountViews")]
        public string CountViews { get; set; }
        [JsonProperty("video")]
        public string Video { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("linkToText")]
        public string LinkToText { get; set; }
        [JsonProperty("singerId")]
        public int? SingerId { get; set; }
        [JsonIgnore]
        public virtual Singer Singer { get; set; }
        [JsonProperty("fingerings")]
        public virtual ICollection<Fingering> Fingerings { get; set; }
        public SuiteСhord()
        {
            Fingerings = new List<Fingering>();
        }
        public SuiteСhord(string name, string countViews, string video, string text, string linkToText, Singer singer)
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