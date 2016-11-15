using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Parser
{
    public class ListFingerings
    {

        public static void GetFingering(string page, Singer singer)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            page = "http:" + page.Substring(0, page.Length - 1);
            doc = hw.Load(page);
            var repeaters = doc.DocumentNode.SelectNodes("//table/tr");//*[@id="tablesort"]/tbody/tr
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        HtmlNode rep = repeater.SelectSingleNode(".//td/a/text()");
                        string name = rep.InnerText;
                        if (name == "RinaOnish")
                        {
                            break;
                        }
                        rep = repeater.SelectSingleNode(".//td/a[@href]");
                        string linkToText = "http:" + rep.Attributes["href"].Value.Substring(0, rep.Attributes["href"].Value.Length - 1);
                        rep = repeater.SelectSingleNode(".//td[@class='number icon']/i[@class='fa fa-youtube-play']");
                        string video;
                        if (rep != null)
                        {
                            video = rep.OuterHtml;
                        }
                        else
                        {
                            video = null;
                        }
                        rep = repeater.SelectSingleNode(".//td[@class = 'number hidden-phone']");
                        string countViews = rep.InnerText;
                        string text = getText(linkToText);
                        context.SuiteСhords.Add(new SuiteСhord(name, countViews, video, text, singer));
                        context.SaveChanges();
                    }
                }
            }
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