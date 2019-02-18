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
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;

            return (date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0') + "/" +
                date.Year.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeightsPage : Page
    {
        IEnumerable<Weight> WeightSource;

        public WeightsPage()
        {
            this.InitializeComponent();

            GetWeightsAsync();
        }

        private async void GetWeightsAsync()
        {
            WeightSource = await DietTrackerAPI.GetWeights(LocalStore.GetSetting("Token"));
            ListViewWeights.ItemsSource = WeightSource;
        }

        private async void ButtonDeleteAsync_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);

            await DietTrackerAPI.DeleteWeight(id, LocalStore.GetSetting("Token"));

            GetWeightsAsync();
        }

        private async void ButtonAddWeight_Click(object sender, RoutedEventArgs e)
        {
            if (WeightSource != null && WeightSource.Count() > 0)
            {
                TextWeight.Text = WeightSource.First<Weight>().userweight.ToString();
            }
            else
            {
                TextWeight.Text = "100";
            }

            CalDatePickerAdd.Date = DateTime.Now;

            ContentDialogResult result = await AddContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await DietTrackerAPI.AddWeight(Convert.ToInt32(LocalStore.GetSetting("UserId")),
                    Convert.ToDouble(TextWeight.Text), CalDatePickerAdd.Date.Value.DateTime, LocalStore.GetSetting("Token"));

                GetWeightsAsync();
            }
            else
            {
                // User pressed Cancel, ESC, or the back arrow.
            }
        }

        private void ButtonWeightSubstract_Click(object sender, RoutedEventArgs e)
        {
            int Quantity = Convert.ToInt32(TextWeight.Text);

            if (Quantity > 1)
            {
                Quantity--;
            }

            TextWeight.Text = Quantity.ToString();
        }

        private void ButtonWeightAdd_Click(object sender, RoutedEventArgs e)
        {
            int Quantity = Convert.ToInt32(TextWeight.Text);

            if (Quantity < 10)
            {
                Quantity++;
            }

            TextWeight.Text = Quantity.ToString();
        }


    }
}
