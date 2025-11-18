
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Abstractions;
using Infrastructure.Common;
using Infrastructure.Persistence;


using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
     this IServiceCollection services,
      IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>()!;

            services.AddDbContext<AppDbContext>(options =>
                                          options.UseNpgsql(dbSettings.ConnectionString));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AppDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddHealthChecks()
                    .AddDbContextCheck<AppDbContext>("Database");

            return services;
        }



    }
}
