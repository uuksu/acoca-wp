using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace Acoca
{
    class Settings
    {
        public static void SetSetting(string name, object value)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            if (!settings.Contains(name))
            {
                settings.Add(name, value);
            }
            else
            {
                settings[name] = value;
            }

            settings.Save();
        }

        public static bool IsNeededSettingsSet()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            // Checking if needed settings has value
            if (settings.Contains("usersWeight") && settings.Contains("usersSex"))
            {
                return true;
            }

            return false;
        }
    }
}
