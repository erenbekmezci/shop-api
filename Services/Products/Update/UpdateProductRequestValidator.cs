using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("isim giriniz")
                .Length(3, 10).WithMessage("isim 3 - 10 karakter arasında olmalı");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalı");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("stok 1 - 100 arasında olmalı");
            RuleFor(x => x.CategoryId)
               .GreaterThan(0).WithMessage("ürün kategori değeri 0'dan büyük olmalıdır.");
        }
    }
}
