using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Parser
{
    public class ListSongs
    {


        public static void GetSongs(HtmlDocument doc, string link)
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
                        using (var context = new ApplicationDbContext())
                        {
                            doc = hw.Load(linkToText);
                            rep = repeater.SelectSingleNode(".//td[@class='number icon']/i[@class='fa fa-youtube-play']");
                            if (!context.SuiteСhords.Any(p => p.LinkToText == linkToText))
                            {
                                string video = GetVideo(rep,doc);
                                string countViews = repeater.SelectSingleNode(".//td[@class = 'number hidden-phone']").InnerText;
                                string text = GetText(doc);
                                context.SuiteСhords.Add(new SuiteСhord(name, countViews, video, text, linkToText, context.Singers.Where(p => p.LinkToSinger == link).First()));
                                context.SaveChanges();
                            }
                            else
                            {
                                SuiteСhord suiteChord = context.SuiteСhords.First(p => p.LinkToText == linkToText);
                                if (suiteChord.Text == null)
                                {
                                    context.Entry(suiteChord).State = EntityState.Modified;
                                    suiteChord.Text = GetText(doc);
                                    context.SaveChanges();
                                }
                                if (suiteChord.Video == null)
                                {
                                    context.Entry(suiteChord).State = EntityState.Modified;
                                    suiteChord.Video = GetVideo(rep,doc);
                                    context.SaveChanges();
                                }
                            }
                            int id = context.SuiteСhords.First(p => p.LinkToText == linkToText).SuiteСhordId;
                            ListFingerings.GetFingering(doc, id);
                        }

                    }
                }
            }
        }

        private static string GetVideo(HtmlNode rep,HtmlDocument doc)
        {
            string video = null;
            HtmlNode bigPicture;
            if (rep != null)
            {
                bigPicture = doc.DocumentNode.SelectSingleNode("//div[@class='b-video-container']/iframe[@src]");
                if (bigPicture != null)
                    video = bigPicture.Attributes["src"].Value;

            }
            return video;
        }

        private static string GetText(HtmlDocument doc)
        {
            var textNode = doc.DocumentNode.SelectSingleNode("//div[@class='b-podbor__text']/pre");
            string text = null;
            if (textNode != null)
            {
               text= HttpUtility.HtmlDecode(textNode.InnerText);
            }
            return text;

        }
    }
}