// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridHFNPage
    {
        public GridHFNViewModel ViewModel { get; }

        public GridHFNPage(IHardwareFilterNameService hardwareFilterNameService, IMapper mapper)
        {
            GridHFNViewModel viewModel = new GridHFNViewModel(new Models.HardwareFilterNameWpf(), hardwareFilterNameService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
