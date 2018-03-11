using Assignment.OnlineRetailStore.Models;
using Assignment.OnlineRetailStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class ProductBusiness : IProductBusiness
    {
        readonly IStoreDbContext _storeDbContext;
        public ProductBusiness()
        {
            _storeDbContext = StoreDbContextFactory.Create();
        }

        public IProduct AddProduct(Product product)
        {
            try
            {
                _storeDbContext.Products.Add(product);
                
                _storeDbContext.Complete();
                
                return product;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                var product = _storeDbContext.Products.Get(productId);
                _storeDbContext.Products.Remove(product);
                _storeDbContext.Complete();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            return _storeDbContext.Products.GetAll();
        }

        public IProduct GetProductById(int productId)
        {
            return _storeDbContext.Products.Find(p => p.ProductId == productId).FirstOrDefault();
        }

        public IProduct UpdateProduct(Product product, int productId)
        {
            try
            {
                var dbproduct = _storeDbContext.Products.Get(productId);
                dbproduct.CategoryId = product.CategoryId;
                dbproduct.InStock = product.InStock;
                dbproduct.ProductName = product.ProductName;
                dbproduct.Price = product.Price;
                _storeDbContext.Complete();
                return product;
            }
            catch
            {
                throw;
            }

        }
    }
}
