// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridDocumentsActReportPage
    {
        public GridDocumentsActReportViewModel ViewModel { get; }

        public GridDocumentsActReportPage(IDocumentsActReportService documentsActReportService,
            IInvertoriesService inventoriesService, IVolumesService volumesService, IMapper mapper)
        {
            GridDocumentsActReportViewModel viewModel = new GridDocumentsActReportViewModel(new DocumentsActReportWpf(), documentsActReportService, inventoriesService, volumesService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
