using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace StockDataBL
{
    public class FromZipStocksDataExtractor
    {
        private readonly FromStringStocksDataExtractor _dataExtractor = new FromStringStocksDataExtractor();
        private WebClient _webClient = new WebClient();

        public IEnumerable<dane_gieldowe> Extract(string uri)
        {
            var list = new List<dane_gieldowe>();
            try
            {
                using (ZipArchive zipArchive = new ZipArchive(new WebClient().OpenRead(uri),ZipArchiveMode.Read))
                {
                    foreach (var zipArchiveEntry in zipArchive.Entries)
                    {
                        using (var stream = zipArchiveEntry.Open())
                        {
                            using (StreamReader str = new StreamReader(stream))
                            {
                                list.AddRange(_dataExtractor.Extract(str.ReadToEnd()));
                            }
                        }
                    }
                }
            }
            catch (WebException e)
            {
                return Enumerable.Empty<dane_gieldowe>();
            }

            return list;
        }
    }
}