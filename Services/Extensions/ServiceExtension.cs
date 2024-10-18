using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Products;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Products;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Services.ExceptionHandlers;
using Services.Categories;

namespace Services.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ICategoryService,CategoryService>();

            services.AddFluentValidationAutoValidation();    //async business validation yaptığımızda .net mvc valid. pipeline desteklemez 
                                                             //senkron çalıştığı için o yüzden service katmanında manuel olarak yapılır her bir 
                                                             //controllerda
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddExceptionHandler<CriticalExceptionHandler>();//sıra önemli burdaki false dönüyor
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
