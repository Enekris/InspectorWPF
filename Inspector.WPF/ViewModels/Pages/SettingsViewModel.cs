// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Newtonsoft.Json.Linq;
using System.IO;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;


namespace Inspector.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private readonly string appSettingsFilePath = "appsettings.json";
        private bool _isInitialized = false;


        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }


        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme();
            //CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
            AppVersion = $"Inspector - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private static string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            string json = File.ReadAllText(appSettingsFilePath);
            JObject jsonObject = JObject.Parse(json);

            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == ApplicationTheme.Light)
                    {
                        break;
                    }

                    ApplicationThemeManager.Apply(ApplicationTheme.Light);
                    CurrentTheme = ApplicationTheme.Light;
                    jsonObject["AppTheme"]["UserTheme"] = "Light";
                    string jsonString = Convert.ToString(jsonObject);
                    File.WriteAllText(appSettingsFilePath, jsonString);
                    break;

                default:
                    if (CurrentTheme == ApplicationTheme.Dark)
                    {
                        break;
                    }

                    jsonObject["AppTheme"]["UserTheme"] = "Dark";
                    string jsonString1 = Convert.ToString(jsonObject);
                    File.WriteAllText(appSettingsFilePath, jsonString1);
                    ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                    CurrentTheme = ApplicationTheme.Dark;

                    break;
            }
        }
    }
}
