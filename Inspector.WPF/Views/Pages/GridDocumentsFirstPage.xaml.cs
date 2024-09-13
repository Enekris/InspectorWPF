// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridDocumentsFirstPage
    {
        public GridDocumentsFirstViewModel ViewModel { get; }

        public GridDocumentsFirstPage(IDocumentsFirstService DocumentsFirstService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            GridDocumentsFirstViewModel viewModel = new GridDocumentsFirstViewModel(new DocumentsFirstWpf(), DocumentsFirstService, inventoriesService, volumesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
