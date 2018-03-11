using Assignment.OnlineRetailStore.Models;
using Assignment.OnlineRetailStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        readonly IStoreDbContext _storeDbContext;
        public OrderBusiness()
        {
            _storeDbContext = StoreDbContextFactory.Create();
        }

        public IOrder AddOrder(Order order)
        {
            try
            {
                _storeDbContext.Orders.Add(order);
                _storeDbContext.Complete();
                return order;
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<IOrder> AddOrder(IEnumerable<Order> orders)
        {
            try
            {
                _storeDbContext.Orders.AddRange(orders);
                _storeDbContext.Complete();
                return orders;
            }
            catch
            {
                return null;
            }
        }

        public void DeleteOrder(Guid orderId)
        {
            try
            {
                var order = _storeDbContext.Orders.Get(orderId);
                _storeDbContext.Orders.Remove(order);
                _storeDbContext.Complete();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<IOrder> GetAllOrders()
        {
            return _storeDbContext.Orders.GetAll();
        }

        public IEnumerable<IOrder> GetOrderById(Guid orderId)
        {
            return _storeDbContext.Orders.GetOrderById(orderId);
        }
          
        public OrderDetails GetOrderDetails(Guid orderId)
        {
            var orderList = _storeDbContext.Orders.GetOrderById(orderId);
            var productList = _storeDbContext.Products.GetAll().Where(s => orderList.Select(p => p.ProductId).Contains(s.ProductId));
            var productCategoryList = _storeDbContext.ProductCategory.GetAll().Where(s => productList.Select(p => p.CategoryId).Contains(s.CategoryId));

            var productDisplayList = productList.Join(productCategoryList, a => a.CategoryId, b => b.CategoryId, (a, b) => new ProductDisplay()
            {
                CategoryName = b.CategoryName,
                Price = orderList.Where(p => p.ProductId == a.ProductId).Select(p => p.Price).FirstOrDefault(),
                SalesTax = b.SalesTax * orderList.Where(p => p.ProductId == a.ProductId).Select(p => p.Quantity).FirstOrDefault(),
                ProductName = a.ProductName,
                Quantity = orderList.Where(p=>p.ProductId == a.ProductId).Select(p=>p.Quantity).FirstOrDefault()
            });

            var orderDetails = new OrderDetails()
            {
                OrderId = orderId,
                ProductDisplayList = productDisplayList.ToList(),
                TotalPrice = productDisplayList.Select(x => x.Price).Sum() + CalculateTax(productDisplayList.Select(x => x.Price).Sum(), productDisplayList.Select(x => x.SalesTax).Sum()),
                TotalTax = productDisplayList.Select(x => x.SalesTax).Sum()
            };

            return orderDetails;
        }
        private double CalculateTax(double price, double saleTax)
        {
            double tax = price * (saleTax/100);
            return tax;
        }

        public IOrder UpdateOrder(Order order, Guid orderId)
        {
            try
            {
                var dborder = _storeDbContext.Orders.Get(orderId);
                //Do Business validations (Skipping here)
                dborder.Price = order.Price;
                dborder.ProductId = order.ProductId;
                dborder.Quantity = order.Quantity;
                _storeDbContext.Complete();
                return order;
            }
            catch
            {
                return null;
            }
        }
    }
}
