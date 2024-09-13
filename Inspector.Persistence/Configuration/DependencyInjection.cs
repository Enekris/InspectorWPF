using Inspector.Application.Contracts.Database.Repositories;
using Inspector.Persistence.DbSettings;
using Inspector.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Inspector.Persistence.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddContext(configuration);
            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<ICabinetsRepository, CabinetsRepository>();
            services.AddScoped<IDocumentsActReportRepository, DocumentsActReportRepository>();
            services.AddScoped<IDocumentsOthersRepository, DocumentsOthersRepository>();
            services.AddScoped<IDocumentsThirdRepository, DocumentsThirdRepository>();
            services.AddScoped<IDocumentsRaspOVVRepository, DocumentsRaspOVVRepository>();
            services.AddScoped<IDocumentsFirstRepository, DocumentsFirstRepository>();
            services.AddScoped<IDocumentsSecondRepository, DocumentsSecondRepository>();
            services.AddScoped<IHardwareFilterNameRepository, HardwareFilterNameRepository>();
            services.AddScoped<IHardwareRepository, HardwareRepository>();
            services.AddScoped<IInvertoriesRepository, InvertoriesRepository>();
            services.AddScoped<IOVTsRepository, OVTsRepository>();
            services.AddScoped<IVolumesRepository, VolumesRepository>();
            //services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<ISertificatesRepository, SertificatesRepository>();

            return services;
        }
        private static IServiceCollection AddContext(
    this IServiceCollection services,
    IConfiguration configuration)
        {
            var config = new ConfigurationBuilder();
            services.AddDbContext<RegistrationOIContext>(builder =>
                    builder.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped);

            return services;
        }

    }
}
