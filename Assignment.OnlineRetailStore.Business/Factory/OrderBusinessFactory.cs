using Assignment.OnlineRetailStore.IoC;
using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class OrderBusinessFactory
    {
        public static IOrderBusiness Create()
        {
            return ObjectLocator.CreateInstance<IOrderBusiness>("Assignment.OnlineRetailStore.Business.OrderBusiness, Assignment.OnlineRetailStore.Business");
        }
    }
}
