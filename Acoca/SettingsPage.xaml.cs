using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace Acoca
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.SetSetting("usersWeight", Double.Parse(WeightTextBox.Text));
            Settings.SetSetting("usersSex", (Sex)SexListBox.SelectedIndex);

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}