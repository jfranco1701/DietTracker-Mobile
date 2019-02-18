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
    public sealed partial class MealFavoriteAddPage : Page
    {
        IEnumerable<Favorite> FavoritesSource;
        String MealType;
        DateTime MealDate;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (AddMealParams)e.Parameter;

            MealDate = parameters.MealDate;
            MealType = parameters.MealType;
        }

        public MealFavoriteAddPage()
        {
            this.InitializeComponent();

            GetFavoritesAsync();
        }

        private async void GetFavoritesAsync()
        {
            FavoritesSource = await DietTrackerAPI.GetFavorites(LocalStore.GetSetting("Token"));
            ListViewFavorites.ItemsSource = FavoritesSource;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            AddMealParams parameters = new AddMealParams();
            parameters.MealType = MealType;
            parameters.MealDate = MealDate;

            Frame.Navigate(typeof(MealsPage), parameters);
        }

        private async void ButtonAddSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (var favoriteItem in ListViewFavorites.Items)
            {
                var listviewitem = favoriteItem as Favorite;
                var container = ListViewFavorites.ContainerFromItem(listviewitem) as ListViewItem;
                var stackpanel = container.ContentTemplateRoot as StackPanel; ;
                var chk = stackpanel.FindName("CheckItem") as CheckBox;

                if (chk.IsChecked.Value)
                {
                    await DietTrackerAPI.AddMeal(Convert.ToInt32(LocalStore.GetSetting("UserId")), listviewitem.foodname, listviewitem.calories,
                    listviewitem.fat, listviewitem.carbs, listviewitem.fiber, listviewitem.sugars, listviewitem.protein, listviewitem.measure,
                    MealDate, MealType, 1, LocalStore.GetSetting("Token"));
                }
            }

            AddMealParams parameters = new AddMealParams();
            parameters.MealType = MealType;
            parameters.MealDate = MealDate;

            Frame.Navigate(typeof(MealsPage), parameters);
        }
    }
}
