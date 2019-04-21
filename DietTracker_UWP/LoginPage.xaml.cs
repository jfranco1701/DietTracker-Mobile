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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            TextBoxEmail.Text = "jdoe@email.com";
            PassBoxPassword.Password = "password";

            TextInvalid.Visibility = Visibility.Collapsed;
            TextLogin.Visibility = Visibility.Collapsed;
            ButtonLogin.Visibility = Visibility.Visible;
            ProgressLogin.Visibility = Visibility.Collapsed;
            
        }

        async void ButtonLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            ProgressLogin.IsActive = true;
            ProgressLogin.Visibility = Visibility.Visible;
            ButtonLogin.Visibility = Visibility.Collapsed;
            TextLogin.Visibility = Visibility.Visible;

            User user = await DietTrackerAPI.Login(TextBoxEmail.Text, PassBoxPassword.Password);

            if (user != null)
            {
                LocalStore.AddSetting("UserID", user.userid.ToString());
                LocalStore.AddSetting("Token", user.token);

                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                TextInvalid.Visibility = Visibility.Visible;
                ProgressLogin.IsActive = false;
                ProgressLogin.Visibility = Visibility.Collapsed;
                ButtonLogin.Visibility = Visibility.Visible;
                TextLogin.Visibility = Visibility.Collapsed;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
