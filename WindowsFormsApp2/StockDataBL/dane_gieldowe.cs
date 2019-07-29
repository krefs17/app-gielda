namespace StockDataBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class dane_gieldowe
    {

        [Column(Order = 0)]
        [StringLength(50)]
        [Key]
        public string nazwa { get; set; }

        [Column(Order = 1)]
        [Key]
        public DateTime data { get; set; }

        public double kurs { get; set; }

        public double otwarcie { get; set; }

        public double min { get; set; }

        public double max { get; set; }

        public double wolumen { get; set; }
    }
}
