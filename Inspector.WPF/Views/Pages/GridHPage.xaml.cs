// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using AutoMapper;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Models;
using Inspector.ViewModels.Pages;

namespace Inspector.Views.Pages
{
    public partial class GridHPage
    {
        public GridHViewModel ViewModel { get; }

        public GridHPage(IHardwaresService hardwaresService,
            IHardwareFilterNameService hardwareFilterNameService, IOVTsService oVTsService,
            IDocumentsFirstService DocumentsFirstService, IDocumentsSecondService DocumentsSecondService,
            IDocumentsThirdService DocumentsThirdService, ICabinetService cabinetService, IMapper mapper)
        {
            GridHViewModel viewModel = new GridHViewModel(new HardwaresWpf(), hardwaresService,
             hardwareFilterNameService, oVTsService,
             DocumentsFirstService, DocumentsSecondService,
             DocumentsThirdService, cabinetService, mapper);
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

    }

}
