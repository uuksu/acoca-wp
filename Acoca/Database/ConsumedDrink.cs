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
    class ConsumedDrink
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public DateTime AddTime { get; set; }

        [Column]
        public int DrinkId { get; set; }

        [Column]
        public int DrinkSessionId { get; set; }
    }
}
