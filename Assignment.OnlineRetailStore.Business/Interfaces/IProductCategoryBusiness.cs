using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public interface IProductCategoryBusiness
    {
        IProductCategory GetProductCategoryById(int productCategoryId);
        IEnumerable<IProductCategory> GetAllProductCategories();

        IProductCategory AddProductCategory(ProductCategory productCategory);
        IProductCategory UpdateProductCategory(ProductCategory productCategory, int productCategoryId);

        void DeleteProductCategory(int productCategoryId);
    }
}
