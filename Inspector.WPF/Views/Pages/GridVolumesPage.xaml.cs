// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridVolumesPage
    {
        public GridVolumesViewModel ViewModel { get; }

        public GridVolumesPage(IVolumesService volumesService, IMapper mapper,
       ISertificatesService sertificatesService)
        {
            GridVolumesViewModel viewModel = new GridVolumesViewModel(new VolumesWpf(), volumesService, mapper, sertificatesService);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
