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
                        using (var context = new ApplicationDbContext())
                        {
                            if (!context.Singers.Any(p => p.LinkToSinger == linkToSinger))
                            {
                                string SingerPage = "http:" + linkToSinger.Substring(0, linkToSinger.Length - 1);
                                doc = hw.Load(SingerPage);
                                string bigPicture = GetSingerPhoto(doc);
                                string biography = GetSingerBiography(doc);
                                string photo = repeater.SelectSingleNode(".//td[@class='photo']/a/img[@src]").Attributes["src"].Value;
                                string name = repeater.SelectSingleNode(".//td[@class='artist_name']/a[@class='artist']").InnerText;
                                string countSongs = repeater.SelectSingleNode("td[@class='number'][1]").InnerText;
                                string countViews = repeater.SelectSingleNode("td[@class='number'][2]").InnerText;
                                context.Singers.Add(new Singer(name, photo, countSongs, countViews, linkToSinger, bigPicture, biography));
                                context.SaveChanges();
                                ListChords.GetChords(doc, linkToSinger);

                                //ListChords.GetChords(linkToSinger, new Singer(name, photo, countSongs, countViews, linkToSinger, bigPicture, biography));
                            }
                            else
                            {
                                ListChords.GetChords(doc, linkToSinger);
                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public static string GetSingerPhoto(HtmlDocument doc)
        {

            var bigPicture = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__photo debug1']/img[@src]");
            if (bigPicture != null)
            {
                return bigPicture.Attributes["src"].Value;
            }
            return null;
        }

        public static string GetSingerBiography(HtmlDocument doc)
        {
            var biography = doc.DocumentNode.SelectSingleNode("//div[@class='artist-profile__bio']/text()");
            if (biography != null)
            {
                return HttpUtility.HtmlDecode(biography.InnerText);
            }
            return null;
        }
    }
}