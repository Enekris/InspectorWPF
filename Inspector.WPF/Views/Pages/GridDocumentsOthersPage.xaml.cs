// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridDocumentsOthersPage
    {
        public GridDocumentsOthersViewModel ViewModel { get; }

        public GridDocumentsOthersPage(IDocumentsOthersService documentsOthersService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            GridDocumentsOthersViewModel viewModel = new GridDocumentsOthersViewModel(new DocumentsOthersWpf(), documentsOthersService, inventoriesService, volumesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
