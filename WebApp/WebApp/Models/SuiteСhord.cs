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
        public int? SingerId { get; set; }
        public Singer Singer { get; set; }
        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Lyrics> ListOfLyrics { get; set; }
        public SuiteСhord()       
        {
            ListOfLyrics = new List<Lyrics>();
        }
    }
}