using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Acoca.Resources;
using Acoca.Database;
using System.Windows.Media;
using System.IO.IsolatedStorage;

namespace Acoca
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                }
            }

            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void DrinkModeChecBox_Checked(object sender, RoutedEventArgs e)
        {
            AddNewDrinkButton.IsEnabled = true;

            if (DrinkSession.GetCurrent() == null)
            {
                DrinkSession.StartNewSession();
            }
        }

        private void DrinkModeChecBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DrinkSession session = DrinkSession.GetCurrent();
            session.EndDrinkingSession();

            AddNewDrinkButton.IsEnabled = false;
        }

        private void AddNewDrinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DrinksPage.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!Settings.IsNeededSettingsSet())
            {
                DrinkModeChecBox.IsEnabled = false;
                DrinkModeInfoTextBlock.Text = "Check your settings first!";
                DrinkModeInfoTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }

            DrinkSession session = DrinkSession.GetCurrent();

            if (session != null)
            {
                AddNewDrinkButton.IsEnabled = true;
                DrinkModeChecBox.IsChecked = true;
                HistoryListBox.ItemsSource = session.GetConsumedDrinks();

                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

                // Calculating users blood alcohol content
                double BAC = AlcoholTools.CalculateBAC((double)settings["usersWeight"],
                            AlcoholTools.CalculateTotalGramsOfAlcohol(session.GetConsumedDrinks().ToList()),
                            (Sex)settings["usersSex"],
                            session.GetTotalDrinkingTime());

                // Calculating total cost of drinking session
                double costs = AlcoholTools.CalculateTotalCosts(session.GetConsumedDrinks().ToList());

                WarningTextBox.Text = DrinkWarning.GetWarningMessage(BAC) ?? "";

                AlcoholLevelText.Text = BAC.ToString("F2");
                SpendMoneyText.Text = costs.ToString("F2");

            }

            base.OnNavigatedTo(e);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}