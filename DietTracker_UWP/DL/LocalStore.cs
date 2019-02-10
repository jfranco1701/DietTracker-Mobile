using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.DL
{
    public static class LocalStore
    {
        public static void AddSetting(String Setting, String Value)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values[Setting] = Value;
        }

        public static String GetSetting(String Setting)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            Object value = localSettings.Values[Setting];

            return value.ToString();
        }
    }
}
