using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLContracts;
using BLContracts.Entities;

namespace StockDataBL
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly string _uriBase = @"https://info.bossa.pl/pub/ciagle/mstock/sesjacgl/";

        private readonly WiborTable _wiborTable = new WiborTable();

        private IEnumerable<string> GenerateCurrentMonthDates()
        {
            var now = DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(now.Year,now.Month);
            return Enumerable.Range(1, daysInMonth)
                .Select(day => new DateTime(now.Year, now.Month, day).ToString("yyyyMMdd"));
        }

        public void UpdateLastWeekStockData()
        {
        }

        public void UpdateLastMonthStockData()
        {
            foreach (var date in GenerateCurrentMonthDates())
            {
                new StocksUpdater().Update(new FromUriStocksDataExtractor()
                    .Extract($"{_uriBase}{date}.prn"));
            }
        }

        public void InsertAllStockData()
        {
            var stocksUpdater = new StocksUpdater();
            stocksUpdater.InsertAll(new FromZipStocksDataExtractor()
                .Extract("https://info.bossa.pl/pub/ciagle/mstock/mstcgl.zip"));
        }

        public string[] GetAllInstrumentsNames()
        {
            using (var context = new StocksDataContext())
            {
                return context.DaneGieldowe.Select(x => x.nazwa).Distinct().ToArray();
            }
        }

        public List<Instrument> GetStockDataFor(params string[] instrumentsNames)
        {
            using (var context = new StocksDataContext())
            {
                return context.DaneGieldowe.Where(dg => instrumentsNames.Contains(dg.nazwa))
                    .GroupBy(dg => dg.nazwa, dg => dg, (nazwa, dane) => new Instrument()
                    {
                        Nazwa = nazwa,
                        Notowania = dane.Select(x => new Notowanie()
                        {
                            Kurs = x.kurs,
                            Max = x.max,
                            Min = x.min,
                            Otwarcie = x.otwarcie,
                            Wolumen = x.wolumen
                        }).ToList()

                    }).ToList();
            }
        }

        public Dictionary<string, float> Wibor => _wiborTable.Wibor;
    }
}