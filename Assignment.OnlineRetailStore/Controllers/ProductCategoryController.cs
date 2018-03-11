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
    public class ProductCategoryController : ApiController
    {
        IProductCategoryBusiness productCategoryBusiness;
        public ProductCategoryController()
        {
            productCategoryBusiness = ProductCategoryBusinessFactory.Create();
        }
        // GET: api/ProductCategory
        public IEnumerable<IProductCategory> Get()
        {
            return productCategoryBusiness.GetAllProductCategories();
        }

        // GET: api/ProductCategory/5
        public IProductCategory Get(int id)
        {
            return productCategoryBusiness.GetProductCategoryById(id);
        }

        // POST: api/ProductCategory
        public IHttpActionResult Post([FromBody]ProductCategory value)
        {
            var productCat = productCategoryBusiness.AddProductCategory(value);
            if (productCat != null)
                return Ok();
            else
                return InternalServerError();
        }

        // PUT: api/ProductCategory/5
        public IHttpActionResult Put(int id, [FromBody]ProductCategory value)
        {
            var productCat = productCategoryBusiness.UpdateProductCategory(value, id);
            if (productCat != null)
                return Ok();
            else
                return InternalServerError();
        }

        // DELETE: api/ProductCategory/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                productCategoryBusiness.DeleteProductCategory(id);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
