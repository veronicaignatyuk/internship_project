using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [JsonObject]
    public class Fingering
    {
        [JsonProperty("fingeringId")]
        public int FingeringId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonIgnore]
        public virtual ICollection<SuiteСhord> SuiteСhords { get; set; }
        public Fingering()
        {
            SuiteСhords = new List<SuiteСhord>();
        }

        public Fingering(string name, string picture) : this()
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