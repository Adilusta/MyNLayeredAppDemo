using DataAccess.Abstract;
using Northwind.Business.Abstract;
using Northwind.Business.Utilities;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.Business.Concretes
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetProducts()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryID == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().StartsWith(productName.ToLower()));
        }

        public void AddProduct(Product product)
        {
            ProductValidator productValidator = new ProductValidator();
         var result =   productValidator.Validate(product);
            if (result.Errors.Count>0)
            {
                throw new ValidationException(result.Errors.Select(x => x.ErrorMessage).Aggregate((a, b) => $"{a}, {b}"));
            }

            _productDal.Add(product);

        }

        public void UpdateProduct(Product product)
        {
            ProductValidator productValidator = new ProductValidator();
            productValidator.Validate(product);
            _productDal.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDal.Delete(product);
        }
    }
}
