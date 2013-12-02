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
    class DrinkSession
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public DateTime StartTime { get; set; }

        [Column]
        public DateTime? EndTime { get; set; }


        public static DrinkSession GetCurrent()
        {
            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                try
                {
                    // Fetching active drink session
                    var drinkSession = db.DrinkingSessions.Where(i => i.StartTime <= DateTime.Now && i.EndTime == null).FirstOrDefault();

                    if (drinkSession != null)
                    {
                        return drinkSession;
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IEnumerable<Drink> GetConsumedDrinks()
        {
            IEnumerable<Drink> drinks;

            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                try
                {
                    // Querying for all consumed drinks
                    var query = db.ConsumedDrinks.Where(i => i.DrinkSessionId == Id)
                        .Join(db.Drinks, drink => drink.DrinkSessionId, consumedDrink => consumedDrink.Id, (consumedDrink, drink) => drink);

                    drinks = query.ToList();

                    return drinks;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static void StartNewSession()
        {
            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                try
                {
                    DrinkSession session = new DrinkSession()
                    {
                        StartTime = DateTime.Now,
                        EndTime = null
                    };

                    db.DrinkingSessions.InsertOnSubmit(session);
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
            }
        }

        public void EndDrinkingSession()
        {
            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                try
                {
                    // Saving current session
                    this.EndTime = DateTime.Now;

                    db.DrinkingSessions.InsertOnSubmit(this);
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                }
            }
        }

        public double GetTotalDrinkingTime()
        {
            return (DateTime.Now - StartTime).TotalHours;
        }
    }
}
