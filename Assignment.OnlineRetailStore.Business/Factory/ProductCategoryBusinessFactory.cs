using Assignment.OnlineRetailStore.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class ProductCategoryBusinessFactory
    {
        public static IProductCategoryBusiness Create()
        {
            return ObjectLocator.CreateInstance<IProductCategoryBusiness>("Assignment.OnlineRetailStore.Business.ProductCategoryBusiness, Assignment.OnlineRetailStore.Business");
        }
    }
}
