using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>,IProductCategoryRepository
    {
        public StoreDbContext StoreDbContext
        {
            get { return Context as StoreDbContext; }
        }

        public ProductCategoryRepository(StoreDbContext context) : base(context)
        {

        }
    }
}
