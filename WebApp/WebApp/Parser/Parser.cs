using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Parser
{
    public class Parser
    {
        private static List<Singer> GetSingers(string page)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            List<Singer> hrefSingers = new List<Singer>();
            for (int i=0; i < 30; i++)
            {

                var link = doc.DocumentNode.SelectSingleNode(@"/html/body/div[@class='content-table']/article[@class='g-padding-left']/table[@class='items']/tbody/tr/tb[@class='artist_name']/a[@href]/");
                HtmlAttribute linkToSinger = link.Attributes["href"];
                var context = new ApplicationDbContext ();
                //проверка на существование в бд linq??
                if (!context.Singers.Where(p => p.LinkToSinger == linkToSinger.Value).Any())
                {
                    link = doc.DocumentNode.SelectSingleNode(@"/html/body/div[@class='content-table']/article[@class='g-padding-left']/table[@class='items']/tbody/tr/tb[@class='photo']/a[@href]/");
                    HtmlAttribute photo = link.Attributes["href"];
                    link = doc.DocumentNode.SelectSingleNode(@"/html/body/div[@class='content-table']/article[@class='g-padding-left']/table[@class='items']/tbody/tr/tb[@class='artist_name']/a[@class='artist']/");
                    HtmlAttribute name = link.Attributes["href"];
                    link = doc.DocumentNode.SelectSingleNode(@"/html/body/div[@class='content-table']/article[@class='g-padding-left']/table[@class='items']/tbody/tr/tb[@class='number']/");
                    HtmlAttribute countSongs = link.Attributes["tb"];
                    hrefSingers.Add(new Singer(photo.Value, name.Value, countSongs.Value, linkToSinger.Value));
                }
            }
            return hrefSingers;
            
        }
    }
}