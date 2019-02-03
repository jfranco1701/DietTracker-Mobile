using DietTracker_UWP.BO;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage;

namespace DietTracker_UWP.DL
{
    public static class DB
    {
        public static async System.Threading.Tasks.Task<List<FoodItem>> QueryDatabaseAsync(String FoodName)
        {
            List<FoodItem> lstFoodItems = new List<FoodItem>();

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


                SqliteCommand selectCommand;
                
                selectCommand = new SqliteCommand("SELECT * FROM FoodItems WHERE FoodName like '%" +
                    FoodName + "%' LIMIT 500", db);

                try
                {
                    query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        FoodItem fooditem = new FoodItem()
                        {
                            NDBNum = Convert.ToInt32(query.GetString(0)),
                            Manufacturer = query.GetString(2),
                            Serving = query.GetString(3),
                            FoodName = query.GetString(1),
                            Calories = Convert.ToDouble(query.GetString(8)),
                            Fat = Convert.ToDouble(query.GetString(9)),
                            Fiber = Convert.ToDouble(query.GetString(7)),
                            Carbs = Convert.ToDouble(query.GetString(6)),
                            Protein = Convert.ToDouble(query.GetString(4)),
                            Sugars = Convert.ToDouble(query.GetString(5))
                        };

                        lstFoodItems.Add(fooditem);
                    }
                }
                catch (SqliteException error)
                {
                    //Handle error
                }

                db.Close();
            }

            return lstFoodItems;
        }
    }
}
