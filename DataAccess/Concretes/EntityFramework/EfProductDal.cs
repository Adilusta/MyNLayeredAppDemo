using DataAccess.Abstract;
using Northwind.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfProductDal :EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal
    { 

    }
}
