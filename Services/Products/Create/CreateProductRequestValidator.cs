using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.Request
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        //her istek olduğunda CreateProductRequestValidator nesnesi oluşur o yüzden yaşam döngüsü scope ve repository enjekte edilebilir.
        private readonly IProductRepository productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(x => x.Name)
                //.NotNull().WithMessage("Name field is required")
                .NotEmpty().WithMessage("Name field is required")
                .Length(3, 10).WithMessage("Name field has to be between 3-10 character");
                //.MustAsync(MustUniqueNameAsync).WithMessage("Name exist in db");
                //.Must(MustUniqueName).WithMessage("Name exist in db"); //fluent validation otomatik paramtreleri sağlıyor metodun imzası yeterli

            //price validation not null olamaz default değeri 0 ancak gidip ? ile not nullable olarak işaretleyebiliriz ama gerek yok
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price has to be greater than 0");
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId has to be greater than 0");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("stock has to be between 1 - 100");
          
        }

        //1.yol senkron method 

        //public bool MustUniqueName(string name)
        //{
        //    return !productRepository.Where(x=>x.Name.Equals(name)).Any();
        //    //"false => hata var"
        //    //"true => hata yok"

        //}

        // manuel fluent validation async olduğu için oto validationı kapattık
        //public async Task<bool> MustUniqueNameAsync(string name , CancellationToken cancellationToken)
        //{
        //    return  !await productRepository.Where(x => x.Name.Equals(name)).AnyAsync(cancellationToken);
        //    //"false => hata var"
        //    //"true => hata yok"

        //}
    }
}
