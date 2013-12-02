using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Acoca.Database;

namespace Acoca
{
    public partial class DrinksPage : PhoneApplicationPage
    {
        public DrinksPage()
        {
            InitializeComponent();
        }

        private void AddNewDrinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddNewDrinkPage.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                    return;
                }

                DrinkListBox.ItemsSource = db.Drinks.OrderBy(i => i.Name).AsEnumerable();
            }
        }

        private void ConsumeDrinkButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                try
                {
                    DrinkSession session = DrinkSession.GetCurrent();

                    // Fetching the wanted drink with tag id saved to button
                    Drink drink = db.Drinks.Where(i => i.Id == (int) button.Tag).First();

                    ConsumedDrink consumedDrink = new ConsumedDrink()
                    {
                        DrinkId = drink.Id,
                        AddTime = DateTime.Now,
                        DrinkSessionId = session.Id
                    };

                    db.ConsumedDrinks.InsertOnSubmit(consumedDrink);

                    db.SubmitChanges();
                }
                catch (Exception)
                {
                    return;
                }
            }

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}