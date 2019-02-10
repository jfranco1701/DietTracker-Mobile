using DietTracker_UWP.BO;
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
using Windows.UI.Xaml.Navigation;
using DietTracker_UWP.DL;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DietTracker_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealAddPage : Page
    {
        List<FoodItem> lstFoodItems = new List<FoodItem>();

        public MealAddPage()
        {
            this.InitializeComponent();

            ListViewMealItems.ItemsSource = lstFoodItems;

            TextBoxSearch.Text = "";
            ButtonAdd.IsEnabled = false;      
        }

        async void Search_ClickAsync(object sender, RoutedEventArgs e)
        {
            lstFoodItems = await DB.QueryDatabaseAsync(TextBoxSearch.Text);

            if (lstFoodItems.Count == 500)
            {
                TextMatches.Text = "Top 500";
            }
            else
            {
                TextMatches.Text = lstFoodItems.Count().ToString();
            }

            ListViewMealItems.ItemsSource = lstFoodItems;
        }

        private void ListViewMealItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewMealItems.SelectedIndex >= 0)
            {
                ButtonAdd.IsEnabled = true;
            }
            else
            {
                ButtonAdd.IsEnabled = false;
            }
        }

        async void ButtonAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            //LocalStore.AddSetting("Token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyLCJ1c2VybmFtZSI6Impkb2VAZW1haWwuY29tIiwiZXhwIjoxNTQ5NzU2MjE2LCJlbWFpbCI6Impkb2VAZW1haWwuY29tIn0.xYvGka8BmqRLlzMniACN3d7DTWsss9wRZ2-2wn0k-3I");
            //LocalStore.AddSetting("UserId", "2");



            String result = await DietTrackerAPI.AddMeal(Convert.ToInt32(LocalStore.GetSetting("UserId")), "addtest", 1.0, 1.0, 1.0, 1.1, 1.1, 1.1, "My Measure", 
                new DateTime(2019,2,9), "Lunch", 1, LocalStore.GetSetting("Token"));
        }
    }
}
