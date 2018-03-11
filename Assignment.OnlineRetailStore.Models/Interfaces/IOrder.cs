using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IOrder
    {
        Guid OrderId { get; set; }
        int ProductId { get; set; }
        double Price { get; set; }
        int Quantity { get; set; }
    }
}
