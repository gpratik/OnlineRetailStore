using Assignment.OnlineRetailStore.Business;
using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment.OnlineRetailStore.Controllers
{
    public class ProductController : ApiController
    {
        IProductBusiness productBusiness;
        public ProductController()
        {
            productBusiness = ProductBusinessFactory.Create();
        }
        // GET: api/Product
        public IEnumerable<IProduct> Get()
        {
            var products = productBusiness.GetAllProducts();
            return products;
        }

        // GET: api/Product/5
        public IProduct Get(int id)
        {
            var product = productBusiness.GetProductById(id);
            return product;
        }

        // POST: api/Product
        public IHttpActionResult Post([FromBody]Product value)
        {
            var product = productBusiness.AddProduct(value);
            if (product != null)
                return Ok();
            else
                return InternalServerError();
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(int id, [FromBody]Product value)
        {
            var product = productBusiness.UpdateProduct(value, id);
            if (product != null)
                return Ok();
            else
                return InternalServerError();
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                productBusiness.DeleteProduct(id);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
