using FluentValidation;
using Northwind.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adı boş geçilemez.");
            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("Birim adedi adı boş geçilemez.");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez.");
            RuleFor(p => p.CategoryID).NotEmpty().WithMessage("Kategori ID boş geçilemez.");
            RuleFor(p => p.UnitsInStock).NotEmpty().WithMessage("Stok miktarı boş geçilemez.");
            RuleFor(p => p.UnitPrice).GreaterThan((decimal)10).WithMessage("Ürün fiyatı 10 dan büyük olmalıdır"); ;
            //RuleFor(p => p.UnitPrice).GreaterThan((decimal)10).When(p => p.CategoryID == 2).WithMessage("Ürün fiyatı 10 dan büyük olmalıdır"); ;
            RuleFor(p => p.ProductName).Must(BeginsWithA).WithMessage("Ürün adı A ile başlamalı");

        }

        private bool BeginsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
