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
                      "?mealdate=" + MealDate.Year.ToString() + "-" + MealDate.Month.ToString().PadLeft(2, '0') + "-" + 
                        MealDate.Day.ToString().PadLeft(2, '0') + "&mealtype=" + MealType);

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
                        "totals/?mealdate=" + MealDate.Year.ToString() + "-" + MealDate.Month.ToString().PadLeft(2, '0') + "-" +
                        MealDate.Day.ToString().PadLeft(2, '0'));

                objMealTotal = JsonConvert.DeserializeObject<MealTotal>(response);
            }

            return objMealTotal;
        }

        public static async Task DeleteMeal(int Id, String Token)
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

        public static async Task DeleteWeight(int Id, String Token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.DeleteAsync(BaseURL + "api/weights/" +
                        Id + "/");
            }
        }

        public static async Task AddWeight(int UserId, double UserWeight, DateTime WeightDate, String Token)
        {
            var content = new StringContent(JsonConvert.SerializeObject(
                new { userid = UserId, userweight = UserWeight,
                    weightdate = WeightDate.Year.ToString() + "-" + WeightDate.Month.ToString().PadLeft(2, '0') + "-" + 
                    WeightDate.Day.ToString().PadLeft(2, '0')}), Encoding.UTF8, "application/json");

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

        public static async Task DeleteFavorite(int Id, String Token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.DeleteAsync(BaseURL + "api/favorites/" +
                        Id + "/");
            }
        }

        public static async Task AddFavorite(int UserId, String FoodName, double CaloriesCount, double FatCount, double CarbsCount,
            double FiberCount, double SugarsCount, double ProteinCount, String MeasureAmt, String Token)
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
                var result = await client.PostAsync(BaseURL + "api/favorites/", content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }
        }

    #endregion
}
}

