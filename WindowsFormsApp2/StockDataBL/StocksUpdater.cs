using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using StockDataBL;

namespace StockDataBL
{
    public class StocksUpdater
    {
        public void Update(IEnumerable<dane_gieldowe> records)
        {
            if (records == null) throw new ArgumentNullException(nameof(records));

            using (var dbContext = new StocksDataContext())
            {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction())
                {
                    dbContext.DaneGieldowe.InsertIfNotExists(records);
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
        }
    }
}