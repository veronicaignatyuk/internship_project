using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Parser
{
    public class Parser
    {
        private static string GetSingers(string page)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            var c = doc.DocumentNode.SelectSingleNode(@"/html/body/table/tbody");

            return "";

            //var tableElements = new List<ReadOnlyCollection<string>>();
            //if (c != null)
            //{
            //    var nodes = c.SelectNodes("//t");
            //    int m = 0;
            //    do
            //    {
            //        var tableRow = new List<string>();
            //        tableRow.Add(nodes[m++].InnerText.Trim());
            //        tableRow.Add(nodes[m++].InnerText.Trim());
            //        tableRow.Add(nodes[m++].InnerText.Trim());
            //        tableElements.Add(tableRow.AsReadOnly());
            //    } while (m < nodes.Count);
            //}
            //return 
            //var nodes = doc.DocumentNode.SelectNodes("//div[@class='browse2-results']/*/div[@class='tt-a']");
            //return nodes
            //    .Cast<HtmlNode>()
            //    .Aggregate(
            //        string.Empty,
            //        (current, node) => current + node.OuterHtml);
        }
    }
}