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
        FoodItem SelectedItem;
        String MealType;
        DateTime MealDate;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (AddMealParams)e.Parameter;

            MealDate = parameters.MealDate;
            MealType = parameters.MealType;
        }

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
                SelectedItem = (FoodItem)e.AddedItems?.FirstOrDefault();
            }
            else
            {
                ButtonAdd.IsEnabled = false;
                SelectedItem = null;
            }
        }

        private async void ShowSaveContentDialogButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await SaveContextDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await DietTrackerAPI.AddMeal(Convert.ToInt32(LocalStore.GetSetting("UserId")), SelectedItem.FoodName, SelectedItem.Calories,
                SelectedItem.Fat, SelectedItem.Carbs, SelectedItem.Fiber, SelectedItem.Sugars, SelectedItem.Protein, SelectedItem.Serving,
                MealDate, MealType, Convert.ToInt32(TextQty.Text), LocalStore.GetSetting("Token"));

                AddMealParams parameters = new AddMealParams();
                parameters.MealType = MealType;
                parameters.MealDate = MealDate;

                Frame.Navigate(typeof(MealsPage), parameters);
            }
            else
            {
                // User pressed Cancel, ESC, or the back arrow.
            }
        }

        private void SaveContextDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            // Ensure that the check box is unchecked each time the dialog opens.
            CheckAddFavorites.IsChecked = false;
            TextQty.Text = "1";
        }

        private void ButtonQtyAdd_Click(object sender, RoutedEventArgs e)
        {
            int Quantity = Convert.ToInt32(TextQty.Text);

            if (Quantity < 10)
            {
                Quantity++;
            }

            TextQty.Text = Quantity.ToString();
        }

        private void ButtonQtySubstract_Click(object sender, RoutedEventArgs e)
        {
            int Quantity = Convert.ToInt32(TextQty.Text);

            if (Quantity > 1)
            {
                Quantity--;
            }

            TextQty.Text = Quantity.ToString();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AddMealParams parameters = new AddMealParams();
            parameters.MealType = MealType;
            parameters.MealDate = MealDate;

            Frame.Navigate(typeof(MealsPage), parameters);
        }
    }
}
;