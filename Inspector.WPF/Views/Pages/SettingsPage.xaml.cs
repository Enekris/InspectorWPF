// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Inspector.ViewModels.Pages;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Controls;
using Wpf.Ui.Controls;
namespace Inspector.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        private readonly string appSettingsFilePath = "appsettings.json";
        public string ConnectionString { get; set; }
        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                pwdTextBox.Text = pwdPasswordBox.Password; // скопируем в TextBox из PasswordBox
                pwdTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
                pwdPasswordBox.Visibility = Visibility.Hidden; // PasswordBox - скрыть
            }
            else
            {
                pwdPasswordBox.Password = pwdTextBox.Text; // скопируем в PasswordBox из TextBox 
                pwdTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                pwdPasswordBox.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }

        private void PwdPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ConnectionString = pwdPasswordBox.Password;
        }

        private void SaveConnectionString_Click(object sender, RoutedEventArgs e)
        {
            string json = File.ReadAllText(appSettingsFilePath);

            JObject jsonObject = JObject.Parse(json);
            jsonObject["ConnectionStrings"]["DefaultConnection"] = ConnectionString;
            string jsonString = Convert.ToString(jsonObject);
            File.WriteAllText(appSettingsFilePath, jsonString);
        }

    }

}
