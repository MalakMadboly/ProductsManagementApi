using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Infrastructure.Persistence;

namespace ProductsManagement.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppdbContext>(options =>
                options.UseSqlite(
                    config.GetConnectionString("DefaultConnection") ??
                    "DataSource=products.db"
                )
            );
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppdbContext>());
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}

