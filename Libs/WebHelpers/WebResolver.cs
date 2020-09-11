using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using CGCCrawler.Libs.Extensions;
using Newtonsoft.Json;

namespace CGCCrawler.Libs.WebHelpers
{
    public static class WebResolver
    {
        /// <summary>
        /// downloads all bitcoin address from web page
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="htmlTag"></param>
        /// <returns></returns>
        public static IList<string> GetAddress(string Host, string htmlTag)
        {
            IList<string> result = null;
            using(HttpClient webClient= new HttpClient())
            {
                string data = webClient.GetStringAsync(Host).Result;
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(data);

                result = new List<string>();

                foreach(HtmlNode node in document.DocumentNode.SelectNodes(htmlTag))
                {
                   if(node.InnerText != "next" && node.InnerText !="previous" && node.InnerText !="+" && node.InnerText != "1LBCPotwPzBvBcTtd7ADGzCWPXXsZE19j6")
                        result.Add(node.InnerText);
                }
            }

            Thread.Sleep(100);

            return result;
        }

        /// <summary>
        /// Checks available balance on bitcoin address
        /// </summary>
        /// <param name="btcAddress"></param>
        /// <returns></returns>
        public static bool CheckAddress(string btcAddress)
        {

            btcAddress = btcAddress.Trim();

            if (btcAddress.Length < 32)
                throw new Exception("Invalid btc address!");

            if (!btcAddress.IsMatch(GVars.REGEX_DIGITS_AND_SYMBOLS))
                throw new Exception("Invalid btc address");

            string btcaddr = string.Format($"{GVars.URL_BLOCKCHAIN}{btcAddress}{GVars.URL_JSON_FORMAT}");

            double balance;

            using (HttpClient webClient= new HttpClient())
            {
                string data = webClient.GetStringAsync(btcaddr).Result;

                var json = JsonConvert.DeserializeObject<dynamic>(data);

                string balanceStr = json.final_balance;

                double.TryParse(balanceStr, out balance);
            }

            if (balance > 0) return true;

            return false;
        }
    }
}
