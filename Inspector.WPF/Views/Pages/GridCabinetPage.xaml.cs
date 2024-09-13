// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridCabinetPage
    {
        public GridCabinetViewModel ViewModel { get; }

        public GridCabinetPage(ICabinetService cabinetService, ISertificatesService sertificatesService,
       IDocumentsRaspOVVService documentsRaspOVVService,
       IDocumentsActReportService documentsActReportService, IMapper mapper, IHardwaresService hardwaresService)
        {
            GridCabinetViewModel viewModel = new GridCabinetViewModel(new CabinetsWpf(), cabinetService, sertificatesService,
        documentsRaspOVVService,
        documentsActReportService, mapper, hardwaresService);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();

        }


    }


}
