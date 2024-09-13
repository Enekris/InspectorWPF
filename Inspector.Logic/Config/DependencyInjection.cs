using Inspector.Application;
using Inspector.Application.Contracts.Logic.Services.Cabinets;
using Inspector.Application.Contracts.Logic.Services.DocumentsActReport;
using Inspector.Application.Contracts.Logic.Services.DocumentsFirst;
using Inspector.Application.Contracts.Logic.Services.DocumentsOthers;
using Inspector.Application.Contracts.Logic.Services.DocumentsRaspOVV;
using Inspector.Application.Contracts.Logic.Services.DocumentsSecond;
using Inspector.Application.Contracts.Logic.Services.DocumentsThird;
using Inspector.Application.Contracts.Logic.Services.HardwareFilterName;
using Inspector.Application.Contracts.Logic.Services.Hardwares;
using Inspector.Application.Contracts.Logic.Services.Invertories;
using Inspector.Application.Contracts.Logic.Services.OVTs;
using Inspector.Application.Contracts.Logic.Services.Sertificates;
using Inspector.Application.Contracts.Logic.Services.Volumes;
using Inspector.Logic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inspector.Logic.Config
{
    public static class DependencyInjection

    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            services.AddScoped<ICabinetService, CabinetService>();
            services.AddScoped<IDocumentsActReportService, DocumentsActReportService>();
            services.AddScoped<IDocumentsOthersService, DocumentsOthersService>();
            services.AddScoped<IDocumentsThirdService, DocumentsThirdService>();
            services.AddScoped<IDocumentsFirstService, DocumentsFirstService>();
            services.AddScoped<IDocumentsSecondService, DocumentsSecondService>();
            services.AddScoped<IDocumentsRaspOVVService, DocumentsRaspOVVService>();
            services.AddScoped<IHardwareFilterNameService, HardwareFilterNameService>();
            services.AddScoped<IHardwaresService, HardwaresService>();
            services.AddScoped<IInvertoriesService, InvertoriesService>();
            services.AddScoped<IOVTsService, OVTsService>();
            services.AddScoped<IVolumesService, VolumesService>();
            services.AddScoped<ISertificatesService, SertificatesService>();
            services.AddAutoMapper(typeof(AppMappingProfile));
            return services;
        }


    }

}
