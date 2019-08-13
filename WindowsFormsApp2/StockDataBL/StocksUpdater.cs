using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;
using MoreLinq;
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

        public void InsertAll(IEnumerable<dane_gieldowe> records)
        {
            if (records == null) throw new ArgumentNullException(nameof(records));
            // var bucktetCount = 1000;
            records = records.DistinctBy(x => new {x.nazwa, x.data}).ToList();
            using (var dbContext = new StocksDataContext())
            {
                dbContext.BulkInsert(records);
//                dbContext.Configuration.AutoDetectChangesEnabled = false;
//                dbContext.Configuration.ValidateOnSaveEnabled = false;
//
//                records.Batch(bucktetCount).ForEach(batch =>
//                {
//                    dbContext.DaneGieldowe.AddRange(batch);
//                    dbContext.SaveChanges();
//
//                });
//  dbContext.DaneGieldowe.AddRange(records);
// dbContext.SaveChanges();
            }
        }
    }
}