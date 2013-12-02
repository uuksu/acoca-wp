using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Acoca.Database
{
    [Table]
    class Drink
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Currency { get; set; }

        [Column]
        public double Value { get; set; }

        [Column]
        public double AlcoholLevel { get; set; }

        [Column]
        public double AmountAsLiters { get; set; }
    }
}
