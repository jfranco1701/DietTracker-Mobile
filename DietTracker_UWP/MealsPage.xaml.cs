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
using DietTracker_UWP.BO;
using DietTracker_UWP.DL;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DietTracker_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealsPage : Page
    {
        public IList<MealItem> BreakfastItems { get; set; }
        public IList<MealItem> LunchItems { get; set; }
        public IList<MealItem> DinnerItems { get; set; }
        public IList<MealItem> SnackItems { get; set; }

        //Allows user control to refresh after a delete
        private event ListUpdateMethod formFunctionPointer;
        delegate void ListUpdateMethod();

        public MealsPage()
        {
            this.InitializeComponent();

            formFunctionPointer += new ListUpdateMethod(GetMealItemsAsync);
            BreakfastFoodList.refreshMethodPointer = formFunctionPointer;
            LunchFoodList.refreshMethodPointer = formFunctionPointer;
            DinnerFoodList.refreshMethodPointer = formFunctionPointer;
            SnackFoodList.refreshMethodPointer = formFunctionPointer;
        }

        private async void GetMealItemsAsync()
        {
            BreakfastItems = await DietTrackerAPI.GetMeals(CalPickerDate.Date.Value.DateTime, "Breakfast", LocalStore.GetSetting("Token"));
            LunchItems = await DietTrackerAPI.GetMeals(CalPickerDate.Date.Value.DateTime, "Lunch", LocalStore.GetSetting("Token"));
            DinnerItems = await DietTrackerAPI.GetMeals(CalPickerDate.Date.Value.DateTime, "Dinner", LocalStore.GetSetting("Token"));
            SnackItems = await DietTrackerAPI.GetMeals(CalPickerDate.Date.Value.DateTime, "Snack", LocalStore.GetSetting("Token"));

            BreakfastFoodList.ItemsSource = BreakfastItems;
            LunchFoodList.ItemsSource = LunchItems;
            DinnerFoodList.ItemsSource = DinnerItems;
            SnackFoodList.ItemsSource = SnackItems;
        }

        void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            CalPickerDate.Date = CalPickerDate.Date.Value.AddDays(-1);
            GetMealItemsAsync();
        }

        void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            CalPickerDate.Date = CalPickerDate.Date.Value.AddDays(1);
            GetMealItemsAsync();
        }

        void ButtonAddItems_Click(object sender, RoutedEventArgs e)
        {
             AddMealParams parameters = new AddMealParams();
            parameters.MealType = ((PivotItem)root.SelectedItem).Header.ToString();
            parameters.MealDate = CalPickerDate.Date.Value.DateTime;

            Frame.Navigate(typeof(MealAddPage), parameters);
        }

        void ButtonAddFavorites_Click(object sender, RoutedEventArgs e)
        {
            AddMealParams parameters = new AddMealParams();
            parameters.MealType = ((PivotItem)root.SelectedItem).Header.ToString();
            parameters.MealDate = CalPickerDate.Date.Value.DateTime;

            Frame.Navigate(typeof(MealFavoriteAddPage), parameters);
        }

        private async void ButtonTotalsAsync_Click(object sender, RoutedEventArgs e)
        {
            MealTotal mealTotal;

            mealTotal = await DietTrackerAPI.GetMealTotals(CalPickerDate.Date.Value.DateTime, LocalStore.GetSetting("Token"));

            TextTotalCalories.Text = mealTotal.total_calories.ToString();
            TextTotalCarbs.Text = mealTotal.total_carbs.ToString();
            TextTotalProtein.Text = mealTotal.total_protein.ToString();
            TextTotalFat.Text = mealTotal.total_fat.ToString();
            TextTotalFiber.Text = mealTotal.total_fiber.ToString(); ;
            TextTotalSugars.Text = mealTotal.total_sugars.ToString();

            await ContentDialogTotals.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var parameters = (AddMealParams)e.Parameter;

                DateTime MealDate = parameters.MealDate;
                String MealType = parameters.MealType;

                CalPickerDate.Date = MealDate;

                switch (MealType)
                {
                    case "Snack": root.SelectedIndex = 3;
                        break;
                    case "Dinner": root.SelectedIndex = 2;
                        break;
                    case "Lunch": root.SelectedIndex = 1;
                        break;
                    default: root.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                CalPickerDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                root.SelectedIndex = 0;
            }

            GetMealItemsAsync();
        }
    }
}
