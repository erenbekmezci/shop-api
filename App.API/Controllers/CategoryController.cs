using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Categories;
using Services.Products.Request;
using Services.Products.Update;
using Services.Products.UpdateStock;
using Services.Products;
using Services.Categories.Create;
using Services.Categories.Update;

namespace App.API.Controllers
{
    
    public class CategoryController(ICategoryService categoryService) : CustomController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetCategoriesWithProduct(int id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));


        [HttpGet("products")]
        public async Task<IActionResult> GetCategoriesWithProduct() => CreateActionResult(await categoryService.GetCategoryWithProductsAsync());


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request) => CreateActionResult(await categoryService.CreateAsync(request));
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request) => CreateActionResult(await categoryService.UpdateAsync(request));


        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id) => CreateActionResult(await categoryService.DeleteAsync(id));



    }
}
