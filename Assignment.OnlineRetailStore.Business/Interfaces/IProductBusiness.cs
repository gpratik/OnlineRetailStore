using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public interface IProductBusiness
    {
        IProduct GetProductById(int productId);
        IEnumerable<IProduct> GetAllProducts();

        IProduct AddProduct(Product product);
        IProduct UpdateProduct(Product product, int productId);

        void DeleteProduct(int productId);
    }
}
