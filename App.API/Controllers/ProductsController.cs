using Microsoft.AspNetCore.Mvc;
using Services.Products;
using Services.Products.Request;
using Services.Products.Update;
using Services.Products.UpdateStock;

namespace App.API.Controllers
{
    public class ProductsController(IProductService productService) : CustomController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());

  

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));


        [HttpGet("{pageNumber:int} / {pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber,pageSize));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(request));
        
        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));


        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
       


    }
}
