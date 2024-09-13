// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridDocumentsRaspOVVPage
    {
        public GridDocumentsRaspOVVModel ViewModel { get; }

        public GridDocumentsRaspOVVPage(IDocumentsRaspOVVService documentsRaspOVVService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            GridDocumentsRaspOVVModel viewModel = new GridDocumentsRaspOVVModel(new DocumentsRaspOVVWpf(), documentsRaspOVVService, inventoriesService, volumesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
