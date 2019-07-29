using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StockDataBL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StocksDataContext : DbContext
    {
        public StocksDataContext()
            : base("name=StocksDataContext")
        {
        }

        public virtual DbSet<dane_gieldowe> DaneGieldowe { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dane_gieldowe>()
                .Property(e => e.nazwa)
                .IsUnicode(false);
        }

      
    }
}