using DRAE_API.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DRAE_API
{
    public class WebScraper
    {
        private readonly static string rae_url = "https://dle.rae.es/";
        private readonly static string listapalabras_url = "https://www.listapalabras.com/";

        private static HtmlDocument LoadPage(string url)
        {
            var Webget = new HtmlWeb();
            Webget.OverrideEncoding = Encoding.GetEncoding("UTF-8");
            return Webget.Load(url);
        }

        public static Entry GetWordMeaning(string word)
        {
            var url = rae_url + word;
            var word_data = LoadPage(url);

            var exist = !word_data.ParsedText.Contains("no está en el Diccionario");

            if (exist)
                return new Entry(word_data, url);
            else
                return null;
        }

        public static List<string> GetAllWordsFromDRAE()
        {
            var list_words = new List<string>();

            var spanish_alphabet = new List<string>()
            {
                "A","B","C","CH","D","E","F","G","H","I","J","K","L","LL","M","N","Ñ","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };

            foreach (var character in spanish_alphabet)
            {
                var url = listapalabras_url + "palabras-con.php?letra=" + character.ToUpper() + "&total=s";
                var word_data = LoadPage(url);

                var result = word_data.GetElementbyId("columna_resultados_generales").Descendants().Where(y => y.Id == "palabra_resultado").Select(p => p.InnerText.Trim()).ToList();
                list_words.AddRange(result);
            }

            return list_words;
        }

        public static List<string> GetAllWordsFromDRAE(string character)
        {
            var url = listapalabras_url + "palabras-con.php?letra=" + character.ToUpper() + "&total=s";
            var word_data = LoadPage(url);

            var result = word_data.GetElementbyId("columna_resultados_generales").Descendants().Where(y => y.Id == "palabra_resultado").Select(p => p.InnerText.Trim()).ToList();

            return result;
        }
    }
}
