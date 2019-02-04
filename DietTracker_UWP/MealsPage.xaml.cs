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


        public MealsPage()
        {
            this.InitializeComponent();

            CalPickerDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            GetMealItemsAsync();
        }

        private async void GetMealItemsAsync()
        {
            List<MealItem> lstMealItems;

            lstMealItems = await DietTrackerAPI.GetMealsAsync(DateTime.Now);

            BreakfastItems = lstMealItems;
            LunchItems = lstMealItems;
            DinnerItems = lstMealItems;
            SnackItems = lstMealItems;

            BreakfastFoodList.ItemsSource = BreakfastItems;
            LunchFoodList.ItemsSource = LunchItems;
            DinnerFoodList.ItemsSource = DinnerItems;
            SnackFoodList.ItemsSource = SnackItems;
        }

        void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            CalPickerDate.Date = CalPickerDate.Date.Value.AddDays(-1);
        }

        void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            CalPickerDate.Date = CalPickerDate.Date.Value.AddDays(1);
        }

        void ButtonAddItems_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MealAddPage));
        }
    }
}
