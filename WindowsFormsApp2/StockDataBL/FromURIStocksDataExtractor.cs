using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace StockDataBL
{
    public class FromUriStocksDataExtractor
    {
        private readonly FromStringStocksDataExtractor _dataExtractor = new FromStringStocksDataExtractor();
        private WebClient _webClient = new WebClient();

        public IEnumerable<dane_gieldowe> Extract(string uri)
        {
            try
            {
                return _dataExtractor.Extract(new WebClient().DownloadString(uri));
            }
            catch (WebException e)
            {
                return Enumerable.Empty<dane_gieldowe>();
            }
        }
    }
}