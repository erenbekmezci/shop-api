using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;
using Services.ExceptionHandlers;
using Services.Products.Create;
using Services.Products.Request;
using Services.Products.Update;
using Services.Products.UpdateStock;
using System.Collections.Generic;
using System.Net;

namespace Services.Products
{
    public class ProductService
        (
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<CreateProductRequest> createProductRequestValidator,
        IMapper mapper
        ) : IProductService
    {
        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            //throw new CriticalException("kritik seviyede bir hata meydana geldi");
            //throw new Exception("dbde hata meydana geldi");

            //async manuel service business check without fluent validation
            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("ürün ismi veritabanında bulunmaktadır.",
                    HttpStatusCode.BadRequest);
            }


            //3. yol async manuel fluent validation
            //var validationResult = await createProductRequestValidator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(x=> x.ErrorMessage).ToList(),
            //        HttpStatusCode.NotFound);   
            //}

            var product = mapper.Map<Product>(request);

            //manuel mapping
            //var product = new Product()
            //{
            //    Name = request.Name,
            //    Stock = request.Stock,
            //    Price = request.Price,
            //};

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessCreated(new CreateProductResponse(product.Id)
                ,$"api/products/{product.Id}"); //product db de oluşturuldu 


        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null) { 
                return ServiceResult.Fail("Product not found" , HttpStatusCode.NotFound);
            }
            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);

        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();
            
            //manuel mapping
            //var productsDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productsDtos = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsDtos);

        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {

                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }
            //manuel mapping
            //var productDto = new ProductDto(product.Id,product.Name,product.Price,product.Stock);

            var productDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto?>.Success(productDto, HttpStatusCode.OK)!;
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();

            //var productsDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productsDtos = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsDtos, HttpStatusCode.OK)!;
        }   

        public async Task<ServiceResult<List<ProductDto>>> GetTopPricesAsync(int count)
        {
            var produts = await productRepository.GetTopPriceProductsAsync(count);

            //var productsDto = produts.Select(product => new ProductDto(product.Id, product.Name, product.Price, 
            //    product.Stock)).ToList();

            var productsDto = mapper.Map<List<ProductDto>>(produts);


            return new ServiceResult<List<ProductDto>>()
            {
                Data = productsDto,
            };
        }

        public  async Task<ServiceResult> UpdateAsync(UpdateProductRequest request)
        {


            var product = await productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
            }


            var isProductNameExist = await productRepository.Where(x => x.Name == request.Name &&x.Id != product.Id).AnyAsync();

            if (isProductNameExist)
            {
                return ServiceResult.Fail("ürün ismi veritabanında bulunmaktadır.",
                    HttpStatusCode.BadRequest);
            }


            //product.Name = request.Name;
            //product.Stock = request.Stock;
            //product.Price = request.Price;

            product = mapper.Map(request,product);

            productRepository.Update(product);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found" , HttpStatusCode.NotFound);
            }

            product.Stock = request.Stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);


            
        }
    }
}
