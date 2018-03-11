using Assignment.OnlineRetailStore.Business;
using Assignment.OnlineRetailStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace Assignment.OnlineRetailStore.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        IOrderBusiness orderBusiness;
        public OrderController()
        {
            orderBusiness = OrderBusinessFactory.Create();
        }
        // GET: api/Order
        public IEnumerable<IOrder> Get()
        {
            return orderBusiness.GetAllOrders();
        }

        // GET: api/Order/5
        public IEnumerable<IOrder> Get(Guid id)
        {
            return orderBusiness.GetOrderById(id);
        }

        [Route("orderDetails/{id}")]
        [HttpGet]
        public IHttpActionResult GetOrderDetails(Guid id)
        {
            var orderDetails =  orderBusiness.GetOrderDetails(id);
            if (orderDetails != null)
            {
                //var response = new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    //Content = new ObjectContent<OrderDetails>(orderDetails, new JsonMediaTypeFormatter())
                //    Content = new StringContent(JsonConvert.SerializeObject(orderDetails))//, Encoding.UTF8, "application/json")
                //};
                
                return new OrderDetailsActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(orderDetails));
            }
            else
                return InternalServerError();

        }

        // POST: api/Order
        public IHttpActionResult Post([FromBody]Order value)
        {
            var order = orderBusiness.AddOrder(value);
            if (order != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<IOrder>(order,new JsonMediaTypeFormatter()) //, Encoding.UTF8, "application/json")
                };
                return Ok(response);
            }
            else
                return InternalServerError();
        }

        [HttpPost]
        [Route("multipleOrders")]
        public IHttpActionResult AddOrders([FromBody]IEnumerable<Order> value)
        {
            var order = orderBusiness.AddOrder(value);
            if (order != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<IEnumerable<IOrder>>(order, new JsonMediaTypeFormatter()) //, Encoding.UTF8, "application/json")
                };
                return Ok(response);
            }
            else
                return InternalServerError();
        }

        // PUT: api/Order/5
        public IHttpActionResult Put(Guid id, [FromBody]Order value)
        {
            var order = orderBusiness.UpdateOrder(value, id);
            if (order != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<IOrder>(order, new JsonMediaTypeFormatter())
                };

                return Ok(response);
            }
            else
                return InternalServerError();
        }

        // DELETE: api/Order/5
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                orderBusiness.DeleteOrder(id);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
