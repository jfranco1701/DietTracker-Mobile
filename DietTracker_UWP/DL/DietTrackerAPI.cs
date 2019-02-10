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
        const String BaseURL = "http://Diettracker-env.yhkmwyss9b.us-east-2.elasticbeanstalk.com/";

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

        public static async Task<User> Login(String Username, String Password)
        {
            User user;

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(
                   new
                   {
                       username = Username,
                       password = Password
                   }), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(BaseURL + "api/login/", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    user = GetUserInfo(result);
                }
                else
                {
                    user = null;
                }
            }

            return user;
        }

        private static User GetUserInfo(String result)
        {
            User user;
            user = JsonConvert.DeserializeObject<User>(result);

            GetTokenInfo(ref user);
            return user;
        }

        private static void GetTokenInfo(ref User user)
        {
            Token token;
            String[] tokenParts = user.token.Split(".");

            var base64EncodedBytes = System.Convert.FromBase64String(tokenParts[1].ToString() + "=");

            token = JsonConvert.DeserializeObject<Token>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));

            user.userid = token.user_id;
            user.username = token.username;
            user.email = token.email;
            user.tokenExpires = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(token.exp).ToLocalTime();
        }

        #region Meals

        public static async Task<List<MealItem>> GetMeals(DateTime MealDate, String MealType, String Token)
        {
            List<MealItem> lstMealItems;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response =
                    await client.GetStringAsync(BaseURL + "api/meals/" +
                      "?mealdate=" + MealDate + "&mealtype=" + MealType);

                lstMealItems = JsonConvert.DeserializeObject<List<MealItem>>(response);
            }

            return lstMealItems;
        }

        public static async Task<MealTotal> GetMealTotals(DateTime MealDate, String Token)
        {
            MealTotal objMealTotal;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response =
                    await client.GetStringAsync(BaseURL + "api/meals/" +
                      "totals/?mealdate=" + MealDate);

                objMealTotal = JsonConvert.DeserializeObject<MealTotal>(response);
            }

            return objMealTotal;
        }

        public static async void DeleteMeal(int Id, String Token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.DeleteAsync(BaseURL + "api/meals/" +
                        Id + "/");
            }
        }
        
        public static async Task<String> AddMeal(int UserId, String FoodName, Double CaloriesCount, Double FatCount, Double CarbsCount,
            Double FiberCount, Double SugarsCount, Double ProteinCount, String MeasureAmt, DateTime MealDate, String MealType, 
            int Quantity, String Token)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                new { userid = UserId,
                      foodname = FoodName,
                      calories = CaloriesCount,
                      fat = FatCount,
                      carbs = CarbsCount,
                      protein = ProteinCount,
                      fiber = FiberCount,
                      sugars = SugarsCount,
                      measure = MeasureAmt,
                      mealdate = MealDate.Year.ToString() + "-" + MealDate.Month.ToString().PadLeft(2,'0') + "-" + MealDate.Day.ToString().PadLeft(2, '0'),
                      mealtype = MealType,
                      quantity = Quantity
                     }), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var result = await client.PostAsync(BaseURL + "api/meals/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
        }

        #endregion

        #region Weight

        public static async Task<List<Weight>> GetWeights(String Token)
        {
            List<Weight> lstWeights;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response =
                    await client.GetStringAsync(BaseURL + "api/weights/");

                lstWeights = JsonConvert.DeserializeObject<List<Weight>>(response);
            }

            return lstWeights;
        }

        public static async void DeleteWeight(int Id, String Token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.DeleteAsync(BaseURL + "api/weights/" +
                        Id + "/");
            }
        }

        public static async void AddWeight(int UserId, double UserWeight, DateTime WeightDate, String Token)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                new { userid = UserId, userweight = UserWeight, weightdate = WeightDate }), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var result = await client.PostAsync(BaseURL + "api/weights/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

        #endregion

        #region Favorites

        public static async Task<List<Favorite>> GetFavorites(String Token)
        {
            List<Favorite> lstFavorites;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response =
                    await client.GetStringAsync(BaseURL + "api/favorites/");

                lstFavorites = JsonConvert.DeserializeObject<List<Favorite>>(response);
            }

            return lstFavorites;
        }

        public static async void DeleteFavorite(int Id, String Token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.DeleteAsync(BaseURL + "api/Favorites/" +
                        Id + "/");
            }
        }

        public static async void AddFavorite(int UserId, String FoodName, int CaloriesCount, int FatCount, int CarbsCount,
            int FiberCount, int SugarsCount, int ProteinCount, String MeasureAmt, String Token)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                 new
                 {
                     userid = UserId,
                     foodname = FoodName,
                     calories = CaloriesCount,
                     fat = FatCount,
                     carbs = CarbsCount,
                     protein = ProteinCount,
                     fiber = FiberCount,
                     sugars = SugarsCount,
                     measure = MeasureAmt
                 }), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var result = await client.PostAsync(BaseURL + "api/Favorites/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

    #endregion
}
}

