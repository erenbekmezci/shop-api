﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetTopPriceProductsAsync(int count);
       

    }
}
