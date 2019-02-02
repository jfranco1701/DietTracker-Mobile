using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Microsoft.Data.Sqlite;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DietTracker_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        async void button_clickAsync(object sender, RoutedEventArgs e)
        {
            await InitializeDatabaseAsync();
        }


        public static async System.Threading.Tasks.Task InitializeDatabaseAsync()
        {
            List<int> nums = new List<int>();
            SqliteDataReader query;

            StorageFile file;
            try
            {
                file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("usda.db");
            }
            catch
            {
                StorageFile Importedfile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/usda.db"));
                file = await Importedfile.CopyAsync(Windows.Storage.ApplicationData.Current.LocalFolder);
            }

            String path = file.Path;

            using (SqliteConnection db = new SqliteConnection("Filename = " + path))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM FoodItems WHERE NDBNum = 10000", db);
                try
                {
                    query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        nums.Add(Convert.ToInt32(query.GetString(0)));
                    }
                }
                catch (SqliteException error)
                {
                    //Handle error
                }

                db.Close();
            }

            



        }
    }
}
