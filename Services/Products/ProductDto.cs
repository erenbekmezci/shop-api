using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products
{
    public record ProductDto(int id , string name , decimal price ,  int stock,int categoryId);

  
}
