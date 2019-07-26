using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLContracts;
using BLContracts.Entities;

namespace StockDataBL
{
    public class BusinessLayer : IBusinessLayer
    {
        public void UpdateLastWeekStockData()
        {
        }

        public void UpdateLastMonthStockData()
        {
        }

        public string[] GetAllInstrumentsNames()
        {
            return Enumerable.Empty<string>().ToArray();
        }

        public List<Instrument> GetStockDataFor(string[] instrumentsNames)
        {
            return Enumerable.Empty<Instrument>().ToList();
        }

        public Dictionary<string, float> Wibor { get; }
    }
}
