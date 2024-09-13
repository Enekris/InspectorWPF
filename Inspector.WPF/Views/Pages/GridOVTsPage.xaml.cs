// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridOVTsPage
    {
        public GridOVTsViewModel ViewModel { get; }

        public GridOVTsPage(IOVTsService oVTsService, ISertificatesService sertificatesServic,
       IDocumentsRaspOVVService documentsRaspOVVService, IMapper mapper)
        {
            GridOVTsViewModel viewModel = new GridOVTsViewModel(new OVTsWpf(), oVTsService, sertificatesServic, documentsRaspOVVService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
