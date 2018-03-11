using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IOrderRepository: IRepository<Order>
    {
        //Include methods required from repository
        IEnumerable<IOrder> GetOrderById(Guid id);
    }
}
