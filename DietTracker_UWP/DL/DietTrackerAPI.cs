using DietTracker_UWP.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.DL
{
    public static class DietTrackerAPI
    {
        public static async System.Threading.Tasks.Task<List<MealItem>> GetMealsAsync(DateTime MealDate)
        {
            List<MealItem> lstMealItems = new List<MealItem>();

            try
            {
                MealItem item = new MealItem() { id = 7, userid = 2, mealdate= new DateTime(2019, 2, 1),
                    mealtype = "Snack", quantity =  1, foodname = "Wheat Thins", calories = 120.00,
                    protein = 2.00, fat = 3.50, fiber = 3.00, carbs = 22.00, sugars = 4.00, measure = "Serving (16 chips)",
                    timestamp = new DateTime(2018, 2, 1,1, 1, 1 )
                };
                MealItem item2 = new MealItem()
                {
                    id = 7,
                    userid = 2,
                    mealdate = new DateTime(2019, 2, 1),
                    mealtype = "Breakfast",
                    quantity = 1,
                    foodname = "Wheat Thins",
                    calories = 120.00,
                    protein = 2.00,
                    fat = 3.50,
                    fiber = 3.00,
                    carbs = 22.00,
                    sugars = 4.00,
                    measure = "Serving (16 chips)",
                    timestamp = new DateTime(2018, 2, 1, 1, 1, 1)
                };
                MealItem item3 = new MealItem()
                {
                    id = 7,
                    userid = 2,
                    mealdate = new DateTime(2019, 2, 1),
                    mealtype = "Lunch",
                    quantity = 1,
                    foodname = "Wheat Thins",
                    calories = 120.00,
                    protein = 2.00,
                    fat = 3.50,
                    fiber = 3.00,
                    carbs = 22.00,
                    sugars = 4.00,
                    measure = "Serving (16 chips)",
                    timestamp = new DateTime(2018, 2, 1, 1, 1, 1)
                };
                MealItem item4 = new MealItem()
                {
                    id = 7,
                    userid = 2,
                    mealdate = new DateTime(2019, 2, 1),
                    mealtype = "Dinner",
                    quantity = 1,
                    foodname = "Wheat Thins",
                    calories = 120.00,
                    protein = 2.00,
                    fat = 3.50,
                    fiber = 3.00,
                    carbs = 22.00,
                    sugars = 4.00,
                    measure = "Serving (16 chips)",
                    timestamp = new DateTime(2018, 2, 1, 1, 1, 1)
                };

                lstMealItems.Add(item);
                lstMealItems.Add(item2);
                lstMealItems.Add(item3);
                lstMealItems.Add(item4);

            }
            catch (Exception ex)
            {
                //Handle error
            }

            return lstMealItems;
        }



        public static async Task<List<MealItem>> GetGreetingAsync()
        {
            List<MealItem> lstMealItems;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response =
                    await client.GetStringAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/meals/");
                // The response object is a string that looks like this:
                // "{ message: 'Hello world!' }"

                lstMealItems = JsonConvert.DeserializeObject<List<MealItem>>(response);
            }

            return lstMealItems;
        }


    }
}

