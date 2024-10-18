

using Services.Products.Create;
using Services.Products.Request;
using Services.Products.Update;
using Services.Products.UpdateStock;

using Services.Categories.Dto;
using Services.Categories.Create;
using Services.Categories.Update;

namespace Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
        Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);

        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
    }
}
