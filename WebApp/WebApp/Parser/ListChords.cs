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


        public static void GetChords(HtmlDocument doc, string link)
        {
            HtmlWeb hw = new HtmlWeb();
            var repeaters = doc.DocumentNode.SelectNodes("//table[@id='tablesort']/tr");//*[@id="tablesort"]/tbody/tr
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        string name = repeater.SelectSingleNode(".//td/a/text()").InnerText;
                        var rep = repeater.SelectSingleNode(".//td/a[@href]");
                        string linkToText = "http:" + rep.Attributes["href"].Value.Substring(0, rep.Attributes["href"].Value.Length - 1);
                        rep = repeater.SelectSingleNode(".//td[@class='number icon']/i[@class='fa fa-youtube-play']");
                        doc = hw.Load(linkToText);
                        string video;
                        if (rep != null)
                        {
                            video = getVideo(doc);
                        }
                        else
                        {
                            video = null;
                        }
                        string countViews = repeater.SelectSingleNode(".//td[@class = 'number hidden-phone']").InnerText;
                        string text = getText(doc);
                        using (var context = new ApplicationDbContext())
                        {
                            context.SuiteСhords.Add(new SuiteСhord(name, countViews, video, text, context.Singers.Where(p => p.LinkToSinger == link).First()));
                            //ListFingerings.GetFingering(linkToText, new SuiteСhord(name, countViews, video, text, singer));
                            context.SaveChanges();
                            int id = context.SuiteСhords.First(p => p.SingerId == context.Singers.FirstOrDefault(s => s.LinkToSinger == link).Singerid).SuiteСhordId;
                            ListFingerings.GetFingering(doc, id);
                        }
                    }
                }
            }
        }

        private static string getVideo(HtmlDocument doc)
        {
            var bigPicture = doc.DocumentNode.SelectSingleNode("//div[@class='b-video-container']/iframe[@src]");
            if (bigPicture != null)
                return bigPicture.Attributes["src"].Value;
            else return null;
        }

        private static string getText(HtmlDocument doc)
        {
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