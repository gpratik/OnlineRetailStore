using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        //Include methods required from repository
    }
}
