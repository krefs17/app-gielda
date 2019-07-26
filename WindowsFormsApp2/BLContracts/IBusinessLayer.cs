using System.Collections.Generic;
using BLContracts.Entities;

namespace BLContracts
{
    public interface IBusinessLayer
    {
        void UpdateLastWeekStockData();
        void UpdateLastMonthStockData();

        string[] GetAllInstrumentsNames();
        List<Instrument> GetStockDataFor(string[] instrumentsNames);

        Dictionary<string,float> Wibor { get; }        
    }
}