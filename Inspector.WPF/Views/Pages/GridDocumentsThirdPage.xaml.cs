// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridDocumentsThirdPage
    {
        public GridDocumentsThirdViewModel ViewModel { get; }

        public GridDocumentsThirdPage(IDocumentsThirdService DocumentsThirdService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            GridDocumentsThirdViewModel viewModel = new GridDocumentsThirdViewModel(new DocumentsThirdWpf(), DocumentsThirdService, inventoriesService, volumesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
