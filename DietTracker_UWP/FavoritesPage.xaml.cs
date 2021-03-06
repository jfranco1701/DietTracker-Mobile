﻿using System;
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
    public sealed partial class FavoritesPage : Page
    {
        IEnumerable<Favorite> FavoritesSource;

        public FavoritesPage()
        {
            this.InitializeComponent();

            GetFavoritesAsync();
        }

        private async void GetFavoritesAsync()
        {
            FavoritesSource = await DietTrackerAPI.GetFavorites(LocalStore.GetSetting("Token"));
            ListViewFavorites.ItemsSource = FavoritesSource;
        }

        private async void ButtonDeleteAsync_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);

            await DietTrackerAPI.DeleteFavorite(id, LocalStore.GetSetting("Token"));

            GetFavoritesAsync();
        }
    }
}
