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
    }
}
