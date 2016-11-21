using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [JsonObject]
    public class Singer
    {
        [JsonProperty("singerid")]
        public int Singerid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("biography")]
        public string Biography { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonProperty("bigPicture")]
        public string BigPicture { get; set; }
        [JsonProperty("countSongs")]
        public string CountSongs { get; set; }
        [JsonProperty("countViews")]
        public string CountViews { get; set; }
        [JsonProperty("linkToSinger")]
        public string LinkToSinger { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuiteСhord> SuiteChords { get; set; }
        public Singer()
        {
            SuiteChords = new List<SuiteСhord>();
        }

        public Singer(string name, string picture, string countSongs, string countViews, string linkToSinger, string bigPicture, string biography)
        {
            this.Name = name;
            this.Picture = picture;
            this.CountSongs = countSongs;
            this.CountViews = countViews;
            this.LinkToSinger = linkToSinger;
            this.BigPicture = bigPicture;
            this.Biography = biography;
        }
    }
}