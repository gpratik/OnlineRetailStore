using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public interface IOrderBusiness
    {
        IEnumerable<IOrder> GetOrderById(Guid orderId);
        IEnumerable<IOrder> GetAllOrders();

        OrderDetails GetOrderDetails(Guid orderId);

        IOrder AddOrder(Order order);
        IEnumerable<IOrder> AddOrder(IEnumerable<Order> orders);
        IOrder UpdateOrder(Order order, Guid orderId);

        void DeleteOrder(Guid orderId);
    }
}
