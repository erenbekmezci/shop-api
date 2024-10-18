using Microsoft.EntityFrameworkCore;
using Repositories.Categories;
using Repositories.Products;
using System.Reflection;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//bu proje klasörü yani assembly (repository)  deki IEntityConfigurationı implemente eden tüm sınıfları al demek

            base.OnModelCreating(builder);
        }
    }

}
