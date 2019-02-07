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

        #region Meals

        public static async Task<List<MealItem>> GetMeals(DateTime MealDate, String MealType)
        {
            List<MealItem> lstMealItems;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response =
                    await client.GetStringAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/meals/" +
                      "?mealdate=" + MealDate + "&mealtype=" + MealType);

                lstMealItems = JsonConvert.DeserializeObject<List<MealItem>>(response);
            }

            return lstMealItems;
        }

        public static async Task<MealTotal> GetMealTotals(DateTime MealDate)
        {
            MealTotal objMealTotal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response =
                    await client.GetStringAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/meals/" +
                      "totals/?mealdate=" + MealDate);

                objMealTotal = JsonConvert.DeserializeObject<MealTotal>(response);
            }

            return objMealTotal;
        }

        public static async void DeleteMeal(int Id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response = await client.DeleteAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/meals/" +
                        Id + "/");
            }
        }
        
        public static async void AddMeal(int UserId, String FoodName, int CaloriesCount, int FatCount, int CarbsCount, 
            int FiberCount, int SugarsCount, int ProteinCount, String MeasureAmt, DateTime MealDate, String MealType, 
            int Quantity)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var result = await client.PostAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/meals/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

        #endregion

        #region Weight

        public static async Task<List<Weight>> GetWeights()
        {
            List<Weight> lstWeights;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response =
                    await client.GetStringAsync("http://diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/weights/");

                lstWeights = JsonConvert.DeserializeObject<List<Weight>>(response);
            }

            return lstWeights;
        }

        public static async void DeleteWeight(int Id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response = await client.DeleteAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/weights/" +
                        Id + "/");
            }
        }

        public static async void AddWeight(int UserId, double UserWeight, DateTime WeightDate)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                new { userid = UserId, userweight = UserWeight, weightdate = WeightDate }), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var result = await client.PostAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/weights/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

        #endregion

        #region Favorites

        public static async Task<List<Favorite>> GetFavorites()
        {
            List<Favorite> lstFavorites;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response =
                    await client.GetStringAsync("http://diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/favorites/");

                lstFavorites = JsonConvert.DeserializeObject<List<Favorite>>(response);
            }

            return lstFavorites;
        }

        public static async void DeleteFavorite(int Id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var response = await client.DeleteAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/Favorites/" +
                        Id + "/");
            }
        }

        public static async void AddFavorite(int UserId, String FoodName, int CaloriesCount, int FatCount, int CarbsCount,
            int FiberCount, int SugarsCount, int ProteinCount, String MeasureAmt, DateTime MealDate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5MjUyNjY0LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.PtsNaGq52P1K7bLxbJfI0JF0hz1clH_vYoJe9fRLeJI");
                var result = await client.PostAsync("http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/api/Favorites/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

    #endregion
}
}

