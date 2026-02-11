using KiiBlog.Application.Services;
using KiiBlog.Application.UnitOfWork;
using KiiBlog.Infrastructure.Options;
using KiiBlog.Infrastructure.Persistence;
using KiiBlog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KiiBlog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Options
            services.Configure<AzureStorageOptions>(configuration.GetSection(AzureStorageOptions.SectionName));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
