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
    public partial class AddNewDrinkPage : PhoneApplicationPage
    {
        public AddNewDrinkPage()
        {
            InitializeComponent();
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            using (AcocaDataContext db = new AcocaDataContext("isostore:/Acoca.sdf"))
            {
                if (db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                }

                double value = 0;
                double alcoholLevel = 0;
                double amountAsLiters = 0;

                try
                {
                    value = Double.Parse(PriceTextBox.Text);
                    alcoholLevel = Double.Parse(AlcoholLevelTextBox.Text);
                    amountAsLiters = Double.Parse(AmountTextBox.Text);
                }
                catch (Exception)
                {
                    return;
                }


                try
                {
                    Drink drink = new Drink();
                    drink.Name = NameTextBox.Text;
                    drink.Value = value;
                    drink.AlcoholLevel = alcoholLevel;
                    drink.AmountAsLiters = amountAsLiters;
                    drink.Currency = "euro";

                    db.Drinks.InsertOnSubmit(drink);

                    db.SubmitChanges();
                }
                catch (Exception)
                {
                    return;
                }
            }

            NavigationService.Navigate(new Uri("/DrinksPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}