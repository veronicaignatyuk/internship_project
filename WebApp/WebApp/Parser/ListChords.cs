using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Parser
{
    public class ListChords
    {

        public static void GetChords(string link)
        {
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            string page = "http:" + link.Substring(0, link.Length - 1);
            doc = hw.Load(page);
            var repeaters = doc.DocumentNode.SelectNodes("//table[@id='tablesort']/tr");//*[@id="tablesort"]/tbody/tr
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        HtmlNode rep = repeater.SelectSingleNode(".//td/a/text()");
                        string name = rep.InnerText;
                        rep = repeater.SelectSingleNode(".//td/a[@href]");
                        string linkToText = "http:" + rep.Attributes["href"].Value.Substring(0, rep.Attributes["href"].Value.Length - 1);
                        rep = repeater.SelectSingleNode(".//td[@class='number icon']/i[@class='fa fa-youtube-play']");
                        string video;
                        if (rep != null)
                        {
                            video = getVideo(linkToText);
                        }
                        else
                        {
                            video = null;
                        }
                        rep = repeater.SelectSingleNode(".//td[@class = 'number hidden-phone']");
                        string countViews = rep.InnerText;
                        string text = getText(linkToText);
                        using(var context = new ApplicationDbContext())
                        {
                            context.SuiteСhords.Add(new SuiteСhord(name, countViews, video, text, context.Singers.Where(p => p.LinkToSinger == link).First()));
                            //ListFingerings.GetFingering(linkToText, new SuiteСhord(name, countViews, video, text, singer));
                            context.SaveChanges();
                            int id = context.SuiteСhords.First(p => p.SingerId == context.Singers.FirstOrDefault(s => s.LinkToSinger == link).Singerid).SuiteСhordId;
                            ListFingerings.GetFingering(linkToText, id);
                        }
                    }
                }
            }
        }

        private static string getVideo(string page)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(page);
            var bigPicture = doc.DocumentNode.SelectSingleNode("//div[@class='b-video-container']/iframe[@src]");
            if (bigPicture != null)
                return bigPicture.Attributes["src"].Value;
            else return null;
        }

        private static string getText(string page)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(page);
            var text = doc.DocumentNode.SelectSingleNode("//div[@class='b-podbor__text']/pre");
            if (text != null)
            {
                return HttpUtility.HtmlDecode(text.InnerText);
            }
            else
            {
                return null;
            }
        }
    }
}