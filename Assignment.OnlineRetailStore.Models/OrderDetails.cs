using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public List<ProductDisplay> ProductDisplayList { get; set; }
        public double TotalPrice { get; set; }
        public double TotalTax { get; set; }

    }
}
