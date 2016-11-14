﻿using HtmlAgilityPack;
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

        public static void GetChords(string page, Singer singer)
        {
            var context = new ApplicationDbContext();
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            page = "http:" + page.Substring(0, page.Length - 1);
            doc = hw.Load(page);
            var repeaters = doc.DocumentNode.SelectNodes("//table/tr");
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        HtmlNode rep = repeater.SelectSingleNode(".//td/a");
                        string name = rep.InnerText;
                        rep = repeater.SelectSingleNode(".//td/a[@href]");
                        string linkToText = "http:" + rep.Attributes["href"].Value.Substring(0, rep.Attributes["href"].Value.Length - 1);
                        rep = repeater.SelectSingleNode(".//td[@class='number icon']/i[@class='fa fa-youtube-play']");
                        string video;
                        if (rep != null)
                        {
                             video = rep.InnerText;
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
            return HttpUtility.HtmlDecode(text.InnerText);
            //*[@id="body"]/div[3]/article/div[1]/div[1]/div[4]/pre/text()[17]
        }
    }
}