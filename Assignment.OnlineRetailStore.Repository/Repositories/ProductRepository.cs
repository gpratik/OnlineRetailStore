using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public StoreDbContext StoreDbContext
        {
            get { return Context as StoreDbContext; }
        }

        public ProductRepository(StoreDbContext context): base(context)
        {
        }

        public IEnumerable<Product> GetTopSellingProducts(int count)
        {
            return StoreDbContext.Products.GetAll().ToList();
        }       

    }
}
