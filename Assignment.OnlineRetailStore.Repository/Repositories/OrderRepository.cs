using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreDbContext context) : base(context)
        {

        }

        public StoreDbContext StoreDbContext
        {
            get { return Context as StoreDbContext; }
        }

        public IEnumerable<IOrder> GetOrderById(Guid id)
        {
            //Multiple extries expected
            return StoreDbContext.Orders.Find(o => o.OrderId == id);
        }
    }
}
