using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StockDataBL
{
    public class FromStringStocksDataExtractor
    {
        private readonly CultureInfo _cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();

        public FromStringStocksDataExtractor()
        {
            _cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
        }
        public IEnumerable<dane_gieldowe> Extract(string source, bool omitHeader = false)
            => source.Split(new char[] {'\n','\r'},StringSplitOptions.RemoveEmptyEntries)
                .Skip(omitHeader ? 1 : 0).Select(
                line =>
                {
                    string[] words = line.Split(',');
                    return new dane_gieldowe()
                    {
                        nazwa = words[0],
                        data = ParseDate(words[1]),
                        otwarcie = double.Parse(words[2], _cultureInfo),
                        max = double.Parse(words[3] , _cultureInfo),
                        min = double.Parse(words[4] , _cultureInfo),
                        kurs = double.Parse(words[5], _cultureInfo),
                        wolumen = double.Parse(words[6], _cultureInfo)
                    };
                });

        private DateTime ParseDate(string word)
        {
            return DateTime.ParseExact(word, "yyyyMMdd", CultureInfo.InvariantCulture);
        }
    }
}