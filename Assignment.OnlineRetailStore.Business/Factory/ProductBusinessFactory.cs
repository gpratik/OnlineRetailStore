using Assignment.OnlineRetailStore.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class ProductBusinessFactory
    {
        public static IProductBusiness Create()
        {
            return ObjectLocator.CreateInstance<IProductBusiness>("Assignment.OnlineRetailStore.Business.ProductBusiness, Assignment.OnlineRetailStore.Business");
        }
    }
}
