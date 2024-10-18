using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository 
    {
      
       
        public ProductRepository(AppDbContext context): base(context) 
        {
              
               
        }
        public  Task<List<Product>> GetTopPriceProductsAsync(int count) 
        {
            
            return  context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync(); 
        }
    }
}
