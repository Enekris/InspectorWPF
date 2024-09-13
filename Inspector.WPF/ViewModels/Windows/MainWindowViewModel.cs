// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Inspector.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Inspector.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Inspector";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems =
  [
              new NavigationViewItem()
              {
                  Content = "ОВТ",
                  Icon = new SymbolIcon { Symbol = SymbolRegular.Desktop20 },
                  TargetPageType = typeof(Views.Pages.GridOVTsPage)

              },
      new NavigationViewItem()
      {
          Content = "Технические средства",
          Icon = new SymbolIcon { Symbol = SymbolRegular.Desktop20 },
          TargetPageType = typeof(Views.Pages.GridHPage)
      },
      new NavigationViewItem()
      {
          Content = "Кабинеты",
          Icon = new SymbolIcon { Symbol = SymbolRegular.ConferenceRoom20 },
          TargetPageType = typeof(Views.Pages.GridCabinetPage)
      },
      new NavigationViewItem()
      {
          Content = "Инвент.ари",
          Icon = new SymbolIcon { Symbol = SymbolRegular.Book20 },
          TargetPageType = typeof(Views.Pages.GridInvertoriesPage)
      },

      new NavigationViewItem()
      {
          Content = "Дела",
          Icon = new SymbolIcon { Symbol = SymbolRegular.Book20 },
          TargetPageType = typeof(Views.Pages.GridVolumesPage)
      },

      new NavigationViewItem("Документы", SymbolRegular.Book20, typeof(GridDocumentsFirstPage), new List<object>
            {
                new NavigationViewItem("Первый", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsFirstPage)),
                new NavigationViewItem("Второй", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsSecondPage)),
                new NavigationViewItem("Третий", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsThirdPage)),
                 new NavigationViewItem("Прочие документы", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsOthersPage)),
                  new NavigationViewItem("Расп. о вводе", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsRaspOVVPage)),
                   new NavigationViewItem("Акт обследования", SymbolRegular.Document20, typeof(Views.Pages.GridDocumentsActReportPage)),
            }),

      new NavigationViewItem()
      {
          Content = "Аттестаты",
          Icon = new SymbolIcon { Symbol = SymbolRegular.Document20 },
          TargetPageType = typeof(Views.Pages.GridSertificatesPage)
      },
      new NavigationViewItem()
      {
          Content = "Технические средства фильтр",
          Icon = new SymbolIcon { Symbol = SymbolRegular.Filter20 },
          TargetPageType = typeof(Views.Pages.GridHFNPage)
      },

      //            new NavigationViewItem()
      //{
      //    Content = "Проверка",
      //    Icon = new SymbolIcon { Symbol = SymbolRegular.Filter20 },
      //    TargetPageType = typeof(Views.Pages.ReportsPage)
      //}
  ];

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems =
        [
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        ];

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems =
        [
            new MenuItem { Header = "Home", Tag = "tray_home" }
        ];
    }
}
