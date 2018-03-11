using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string ProductName { get; set; }
        int CategoryId { get; set; }
        bool InStock { get; set; }
        double Price { get; set; }
    }
}
