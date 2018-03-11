using Assignment.OnlineRetailStore.Models;
using Assignment.OnlineRetailStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class ProductCategoryBusiness : IProductCategoryBusiness
    {
        readonly IStoreDbContext _storeDbContext;
        public ProductCategoryBusiness()
        {
            _storeDbContext = StoreDbContextFactory.Create();
        }

        public IProductCategory AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                _storeDbContext.ProductCategory.Add(productCategory);
                return productCategory;
            }
            catch { return null; }
        }

        public void DeleteProductCategory(int productCategoryId)
        {
            try
            {
                var productCategory = _storeDbContext.ProductCategory.Get(productCategoryId);
                _storeDbContext.ProductCategory.Remove(productCategory);
                _storeDbContext.Complete();
            }
            catch { throw; }
        }

        public IEnumerable<IProductCategory> GetAllProductCategories()
        {
            return _storeDbContext.ProductCategory.GetAll();
        }

        public IProductCategory GetProductCategoryById(int productCategoryId)
        {
            return _storeDbContext.ProductCategory.Get(productCategoryId);
        }

        public IProductCategory UpdateProductCategory(ProductCategory productCategory, int productCategoryId)
        {
            try
            {
                var dbProdCateg = _storeDbContext.ProductCategory.Get(productCategoryId);
                dbProdCateg.CategoryName = productCategory.CategoryName;
                dbProdCateg.SalesTax = productCategory.SalesTax;
                _storeDbContext.Complete();
                return productCategory;
            }
            catch { return null; }
        }
    }
}
