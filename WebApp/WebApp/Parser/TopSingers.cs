using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Parser
{
    public class TopSingers
    {

        public static void GetSingers(string page)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(page);
            var repeaters = doc.DocumentNode.SelectNodes("//table[@class='items']/tr");
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        HtmlNode rep = repeater.SelectSingleNode(".//td[@class='artist_name']/a[@href]");
                        string linkToSinger = rep.Attributes["href"].Value;
                        if (!context.Singers.Any(p => p.LinkToSinger == linkToSinger))
                        {
                            string bigPicture = GetSingerPhoto(linkToSinger);
                            string biography = GetSingerBiography(linkToSinger);
                            rep = repeater.SelectSingleNode(".//td[@class='photo']/a/img[@src]");
                            string photo = rep.Attributes["src"].Value;
                            rep = repeater.SelectSingleNode(".//td[@class='artist_name']/a[@class='artist']");
                            string name = rep.InnerText;
                            rep = repeater.SelectSingleNode("td[@class='number'][1]");
                            string countSongs = rep.InnerText;
                            rep = repeater.SelectSingleNode("td[@class='number'][2]");
                            string countViews = rep.InnerText;
                            context.Singers.Add(new Singer(name, photo, countSongs, countViews, linkToSinger, bigPicture, biography));
                            ListChords.GetChords(linkToSinger, new Singer(name, photo, countSongs, countViews, linkToSinger, bigPicture, biography));
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public static string GetSingerPhoto(string page)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            page = "http:" + page.Substring(0, page.Length - 1);
            doc = hw.Load(page);
            var bigPicture = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__photo debug1']/img[@src]");
            return bigPicture.Attributes["src"].Value; 
        }

        public static string GetSingerBiography(string page)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            page = "http:" + page.Substring(0, page.Length - 1);
            doc = hw.Load(page); 
            var biography = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']/text()");
            if(biography != null)
            {
                return HttpUtility.HtmlDecode(biography.InnerText);
            }
            return null;
        }
    }
}