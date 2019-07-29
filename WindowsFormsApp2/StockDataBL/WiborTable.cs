using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace StockDataBL
{
    public class WiborTable
    {
        private DateTime? _wiborLastUpdate;
        private Dictionary<string, float> _wibor = new Dictionary<string, float>();

        public Dictionary<string, float> Wibor
        {
            get
            {
                if(!_wiborLastUpdate.HasValue || _wiborLastUpdate.Value.Date != DateTime.Today)
                    UpdateWiborDictionary();
                return _wibor;
            }
        }

        private void UpdateWiborDictionary()
        {
            _wibor.Clear();
            string updateUrl = "https://www.bankier.pl/mieszkaniowe/stopy-procentowe/wibor";
            _wiborLastUpdate = DateTime.Today;

            WebClient client = new WebClient();
            var html = client.DownloadString(updateUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='summaryTable']");
            foreach (var row in table.Descendants("tr").Skip(1))
            {
                string name = row.Descendants("a").SingleOrDefault()?.InnerText;
                if (name == null)
                    break;
                string valueString = row.ChildNodes.SingleOrDefault(x=> x.Name == "td" && x.Attributes.Any(y => y.Name=="class"))?.GetDirectInnerText();
                valueString = new string (valueString.Trim().AsEnumerable().TakeWhile(c=>c!='%').ToArray());
                float value = float.Parse(valueString);
                _wibor.Add(name,value);
            }
        }
    }
}