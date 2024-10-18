using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Categories;
using Repositories.Interceptors;
using Repositories.Products;

namespace Repositories.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositorie(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                option.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
                option.AddInterceptors(new AuditDbContextInterceptor());

            });
            services.AddScoped<IProductRepository , ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
         

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));//?
            services.AddScoped<IUnitOfWork , UnitOfWork>();

            return services;
        }
    }
}
