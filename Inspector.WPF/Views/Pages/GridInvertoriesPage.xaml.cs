// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridInvertoriesPage
    {
        public GridInvertoriesViewModel ViewModel { get; }

        public GridInvertoriesPage(IInvertoriesService inventoriesService, IMapper mapper)
        {
            GridInvertoriesViewModel viewModel = new GridInvertoriesViewModel(new InvertoriesWpf(), inventoriesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
