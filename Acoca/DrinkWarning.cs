using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acoca
{
    public static class DrinkWarning
    {
        public static string GetWarningMessage(double BAC)
        {
            if (BAC >= 0.5)
            {
                return "Warning! You're unable to operate motorized vehicle!"; 
            }

            return null;
        }
    }
}
