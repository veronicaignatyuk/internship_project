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

        public static void GetFingering(string page, int id)
        {
            HtmlDocument doc = new HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(page);
            var repeaters = doc.DocumentNode.SelectNodes("//div[@id='song_chords']");
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        string name = repeater.SelectSingleNode(".//img[@alt]").Attributes["alt"].Value;

                        name = name.Substring(7, name.Length - 7);
                        using (var context = new ApplicationDbContext())
                        {
                            if (!context.Fingerings.Any(p => p.Name == name))
                            {
                                string picture = "http:" + repeater.SelectSingleNode(".//img[@src]").Attributes["src"].Value;
                                SuiteСhord suiteChord = context.SuiteСhords.Where(p => p.SuiteСhordId == id).First();
                                suiteChord.Fingerings.Add(new Fingering(name, picture, context.SuiteСhords.Where(p => p.SuiteСhordId == id).First()));
                                context.SuiteСhords.Add(suiteChord);
                            }
                            else
                            {
                                //string picture = "http:" + repeater.SelectSingleNode(".//img[@src]").Attributes["src"].Value;
                                Fingering fingering = context.Fingerings.First(p => p.Name == name);
                                SuiteСhord suiteChord = context.SuiteСhords.Where(p => p.SuiteСhordId == id).First();
                                suiteChord.Singer = context.Singers.Where(p => p.Singerid == suiteChord.SingerId).First();
                                suiteChord.Fingerings.Add(fingering);
                                context.SuiteСhords.Add(suiteChord);

                            }
                            context.SaveChanges();
                        }
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