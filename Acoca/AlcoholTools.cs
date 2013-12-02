using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acoca.Database;

namespace Acoca
{
    class AlcoholTools
    {
        public static double CalculateBAC(double bodyWeight, double alcoholVolumeAsGrams, Sex sex, double hours)
        {
            double alcoholAfterBurning = alcoholVolumeAsGrams - (hours * (0.1 * bodyWeight));
            double BAC = alcoholAfterBurning / (((sex == Sex.Male) ? 0.75 : 0.66) * bodyWeight);
            return BAC;
        }

        public static double CalculateGramsOfAlcoholInDrink(double alcoholVolumeAsLiters, double ABVPercent)
        {
            return (alcoholVolumeAsLiters * ABVPercent) * 0.798;
        }

        public static double CalculateTotalGramsOfAlcohol(List<Drink> drinks)
        {
            double total = 0;

            foreach (Drink drink in drinks)
            {
                total += CalculateGramsOfAlcoholInDrink(drink.AmountAsLiters, drink.AlcoholLevel);
            }

            return total * 10;
        }

        public static double CalculateTotalCosts(List<Drink> drinks)
        {
            double total = 0;

            foreach (Drink drink in drinks)
            {
                total += drink.Value;
            }

            return total;
        }
    }
}
