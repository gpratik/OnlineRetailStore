using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public class ProductCategory : IProductCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double SalesTax { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}
