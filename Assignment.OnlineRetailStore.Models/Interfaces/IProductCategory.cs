using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Models
{
    public interface IProductCategory
    {
        int CategoryId { get; set; }
        string CategoryName { get; set; }

        double SalesTax { get; set; }

        //We can consider this, for now its beyond scope of assignment
        //int ParentCategory { get; set; }

    }
}
