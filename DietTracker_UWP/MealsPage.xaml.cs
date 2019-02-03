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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DietTracker_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MealsPage : Page
    {
        public IList<FoodItem> Items { get; set; }

        List<FoodItem> lstBreakfast;
        List<FoodItem> lstLunch;
        List<FoodItem> lstDinner;
        List<FoodItem> lstSnack;

        public MealsPage()
        {
            this.InitializeComponent();

            CalPickerDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            GetMealItems();
        }

        private void GetMealItems()
        {
            //Get the day's meal items from the API
            lstBreakfast = new List<FoodItem>();
            lstLunch = new List<FoodItem>();
            lstDinner = new List<FoodItem>();
            lstSnack = new List<FoodItem>();

            FoodItem test = new FoodItem() { FoodName = "Test food" };
            lstBreakfast.Add(test);



            //BreakfastFoodList.ItemsSource = lstBreakfast;

            Items = lstBreakfast;
            BreakfastFoodList.ItemsSource = Items;

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
