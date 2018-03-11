using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IProductRepository:IRepository<Product>
    {
        //Include methods required from repository
        //Below method is not in use example for design only
        IEnumerable<Product> GetTopSellingProducts(int count);
    }
}
