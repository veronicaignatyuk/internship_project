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
    public class ListFingerings
    {

        public static void GetFingering(HtmlDocument doc, int id)
        {
            var repeaters = doc.DocumentNode.SelectNodes("//div[@id='song_chords']/img");
            if (repeaters != null)
            {
                foreach (var repeater in repeaters)
                {
                    if (repeater != null)
                    {
                        string name = repeater.Attributes["alt"].Value;
                        name = name.Substring(7, name.Length - 7);
                        using (var context = new ApplicationDbContext())
                        {
                            if (!context.Fingerings.Any(p => p.Name == name))
                            {
                                string picture = "http:" + repeater.Attributes["src"].Value;
                                SuiteСhord suiteChord = context.SuiteСhords.First(p => p.SuiteСhordId == id);
                                Fingering fingering = context.Fingerings.Add(new Fingering(name, picture, suiteChord));
                                context.SaveChanges();
                                context.Entry(suiteChord).State = EntityState.Modified;
                                suiteChord.Fingerings.Add(fingering);
                                context.SaveChanges();
                            }
                            else
                            {
                                //string picture = "http:" + repeater.SelectSingleNode(".//img[@src]").Attributes["src"].Value;
                                Fingering fingering = context.Fingerings.First(p => p.Name == name);
                                SuiteСhord suiteChord = context.SuiteСhords.First(p => p.SuiteСhordId == id);
                                context.Entry(suiteChord).State = EntityState.Modified;
                                context.Entry(fingering).State = EntityState.Modified;
                                suiteChord.Fingerings.Add(fingering);
                                fingering.SuiteСhords.Add(suiteChord);
                                context.SaveChanges();

                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}