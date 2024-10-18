

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Categories;
using Repositories.Products;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;
using Services.Products.Create;
using System.Net;

namespace Services.Categories
{
    public class CategoryService
        (
        ICategoryRepository categoryRepository , 
        IUnitOfWork unitOfWork, 
        IMapper mapper
        ) : ICategoryService
    {
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.Where(x=> x.Name == request.name).AnyAsync();
            if(anyCategory)
            {
                return ServiceResult<int>.Fail("kategori ismi veritabanında bulunmaktadır.",
                    HttpStatusCode.BadRequest);
            }

            var category = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<int>.SuccessCreated(category.Id, $"api/categories/{category.Id}");

        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync( id );
            if (category == null)
            {
                return ServiceResult.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }

            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesDto);

        }

        public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return ServiceResult<CategoryDto?>.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto?>.Success(categoryDto);

        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("kategori bulunamadı" , HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryDto);

        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync().ToListAsync();

            if (category == null)
            {
                return ServiceResult<List<CategoryWithProductsDto>>.Fail("kategoriler bulunamadı", HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<List<CategoryWithProductsDto>>(category);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryDto);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(request.id);

            if (category is null)
            {
                return ServiceResult.Fail("category not found", HttpStatusCode.NotFound);
            }


            var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.name && x.Id != category.Id).AnyAsync();

            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("kategori ismi veritabanında bulunmaktadır.",
                    HttpStatusCode.BadRequest);
            }


            

            category = mapper.Map(request,category);

            categoryRepository.Update(category);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
