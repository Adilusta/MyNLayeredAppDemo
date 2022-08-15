using DataAccess.Abstract;
using Northwind.Business.Abstract;
using Northwind.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this._categoryDal = categoryDal;
        }

        public List<Category> GetCategories()
        {
            return _categoryDal.GetAll();
        }
    }
}
