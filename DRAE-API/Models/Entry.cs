using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DRAE_API.Models
{
    public class Entry
    {
        private HtmlDocument html_data { get; set; }
        public Entry(HtmlDocument html_data, string URL)
        {
            this.html_data = html_data;
            this.URL = URL;
        }

        public string URL { get; set; }

        public string Word
        {
            get
            {
                return html_data.DocumentNode.Descendants().Where(node => node.Name == "title").FirstOrDefault().InnerHtml.Split('|')[0].Trim();
            }
        }

        public List<string> Definitions
        {
            get
            {
                return Regex.Split(string.Join("", html_data.DocumentNode.SelectNodes("//p[@class='j']").Descendants().Where(w => w.Name == "#text" && w.ParentNode.Name != "abbr").Select(z => z.InnerText).ToList()), @"([\d]+\. )(.*?)(?=([\d]+\.)|($))").Select(q => q.Trim()).Where(o => o != "").Select(z => z.TrimEnd('.')).Where(w => w.Any(c => !char.IsDigit(c))).Select(p => HttpUtility.HtmlDecode(p) + ".").ToList();
            }
        }
    }
}
