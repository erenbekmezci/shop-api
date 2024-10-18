using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public IQueryable<Category> GetCategoryWithProductsAsync()
        {
            return context.Categories.Include(c => c.Products).AsQueryable();
        }

        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {            
            return await context.Categories.Include(c => c.Products).FirstOrDefaultAsync();       
        }


    }
}
