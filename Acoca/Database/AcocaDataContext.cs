using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Acoca.Database
{
    class AcocaDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/Acoca.sdf";

        public AcocaDataContext(string connectionString): base(connectionString) { }

        public Table<Drink> Drinks;

        public Table<ConsumedDrink> ConsumedDrinks;

        public Table<DrinkSession> DrinkingSessions;
    }
}
