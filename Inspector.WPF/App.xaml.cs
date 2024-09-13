// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Inspector.Config;
using Inspector.Logic.Config;
using Inspector.Persistence.Configuration;
using Inspector.Services;
using Inspector.ViewModels.Pages;
using Inspector.ViewModels.Windows;
using Inspector.ViewModels.Windows.CabinetsTree;
using Inspector.ViewModels.Windows.VolumesTree;
using Inspector.Views.Exception;
using Inspector.Views.Pages;
using Inspector.Views.TreeViewPages;
using Inspector.Views.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Threading;
using Wpf.Ui;
namespace Inspector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly string appSettingsFilePath = "appsettings.json";
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Service containing navigation, same as INavigationWindow... but without window
                services.AddSingleton<INavigationService, NavigationService>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();



                var configuration = context.Configuration;
                services.AddPersistence(configuration);
                services.AddLogic();
                services.AddAutoMapper(typeof(WpfMappingProfile));

                services.AddScoped<GridDocumentsRaspOVVPage>();
                services.AddScoped<GridDocumentsRaspOVVModel>();

                services.AddScoped<GridDocumentsActReportPage>();
                services.AddScoped<GridDocumentsActReportViewModel>();

                services.AddScoped<GridDocumentsThirdPage>();
                services.AddScoped<GridDocumentsThirdViewModel>();

                services.AddScoped<GridDocumentsFirstPage>();
                services.AddScoped<GridDocumentsFirstViewModel>();

                services.AddScoped<GridDocumentsSecondPage>();
                services.AddScoped<GridDocumentsSecondViewModel>();

                services.AddScoped<GridDocumentsOthersPage>();
                services.AddScoped<GridDocumentsOthersViewModel>();

                services.AddScoped<GridVolumesViewModel>();
                services.AddScoped<GridVolumesPage>();

                services.AddScoped<GridSertificatesViewModel>();
                services.AddScoped<GridSertificatesPage>();

                services.AddScoped<GridOVTsViewModel>();
                services.AddScoped<GridOVTsPage>();

                services.AddScoped<GridInvertoriesViewModel>();
                services.AddScoped<GridInvertoriesPage>();

                services.AddScoped<GridCabinetViewModel>();
                services.AddScoped<GridCabinetPage>();

                services.AddScoped<ExceptionViewModel>();
                services.AddScoped<ExceptionPage>();

                services.AddScoped<GridHPage>();
                services.AddScoped<GridHViewModel>();

                services.AddScoped<GridHFNPage>();
                services.AddScoped<GridHFNViewModel>();

                services.AddScoped<SettingsPage>();
                services.AddScoped<SettingsViewModel>();

                services.AddScoped<TreeVolumesViewModel>();
                services.AddScoped<TreeViewPageVolumes>();

                services.AddScoped<TreeCabinetsViewModel>();
                services.AddScoped<TreeViewPageCabinets>();

            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        private void CheckTheme()
        {

            string json = File.ReadAllText(appSettingsFilePath);
            JObject jsonObject = JObject.Parse(json);
            var theme = Convert.ToString(jsonObject["AppTheme"]["UserTheme"]);
            if (theme == "Dark")
            {
                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                                Wpf.Ui.Appearance.ApplicationTheme.Dark,
                                Wpf.Ui.Controls.WindowBackdropType.Mica,
                                false
                               );
            }
            else if (theme == "Light")
            {
                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                                Wpf.Ui.Appearance.ApplicationTheme.Light,
                                Wpf.Ui.Controls.WindowBackdropType.Mica,
                                false
                               );
            }
            else
            {
                Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
                                Wpf.Ui.Appearance.ApplicationTheme.Dark,
                                Wpf.Ui.Controls.WindowBackdropType.Mica,
                                false
                               );
            }

        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            CheckTheme();
            _host.Start();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ErrorHandler.ShowError(e.Exception, "Ошибка");

            e.Handled = true;
        }
    }
}
