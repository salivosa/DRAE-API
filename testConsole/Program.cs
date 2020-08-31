using System;
using System.Configuration;
using System.Linq;
using DRAE_API;


namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var entry = WebScraper.GetWordMeaning("gringo");
            //var test = WebScraper.GetAllWordsFromDRAE();
            //var test2 = WebScraper.GetAllWordsFromDRAE("CH");
            //var test3 = WebScraper.GetAllWordsFromDRAE('s').Where(x=> x.Contains("sali")).Select(w => WebScraper.GetWordMeaning(w)).ToList();
        }
    }
}
