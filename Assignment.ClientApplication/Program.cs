using Assignment.OnlineRetailStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ClientApplication
{
    class Program
    {
        static HttpClient client = new HttpClient();
        public static string Tab { get { return "\t"; } }
        static List<Product> lstProducts;
        static Guid currentOrderId;
        //This is lamest client to test (Web Client in progress)
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to Online Retail Store");
                Console.WriteLine("Loading Product List ...");
                client.BaseAddress = new Uri("http://localhost:1320");
                DisplayAllProducts();
                Console.WriteLine("Press 1 to create a new Order, Press any key to exit");
                if (int.TryParse(Console.ReadLine(), out int newOrderCode))
                {
                    if (newOrderCode == 1)
                        CreateNewOrder();
                    else
                    {
                        Console.WriteLine("Thanks for Visiting store");
                    }
                }
                //currentOrderId = new Guid("30369fa5-906e-47cc-a15e-50411201053d");
                //FetchOrderDetails();

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fatal Error occured");
            }
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
        static void CreateNewOrder()
        {
            currentOrderId = Guid.NewGuid();
            AddProducts();

        }
        static void AddProducts()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Add products to cart");
            List<Order> listorders = new List<Order>();
            bool exit = true;
            try
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Scan/Enter Product ID");
                    int.TryParse(Console.ReadLine(), out int productId);
                    //Validation
                    if (lstProducts.FirstOrDefault(x => x.ProductId == productId) != null)
                    {
                        Console.WriteLine("Scan/Enter Product Quantity");
                        int.TryParse(Console.ReadLine(), out int productQuantity);
                        //Console.WriteLine("Adding Product to order list");
                        var objData = new Order()
                        {
                            OrderId = currentOrderId,
                            Price = lstProducts.FirstOrDefault(x => x.ProductId == productId).Price * productQuantity,
                            Quantity = productQuantity,
                            ProductId = productId
                        };
                        listorders.Add(objData);
                        Console.WriteLine("Press 1 to generate bill, else press any key to add more products");
                        int.TryParse(Console.ReadLine(), out int Exitcode);
                        if (Exitcode == 1)
                        {
                            exit = false;
                        }                       

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No Matching Products found");
                    }

                } while (exit);
                AddAllProductOrders(listorders);

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Add Product Error occured");
            }
        }
        static void AddAllProductOrders(List<Order> lstOrder)
        {
            var result = client.PostAsync("api/order/multipleOrders", new StringContent(JsonConvert.SerializeObject(lstOrder), Encoding.UTF8, "application/json")).Result;
            if (result.IsSuccessStatusCode)
            {
                FetchOrderDetails();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Add All Orders Error occured");
            }
        }
        static void FetchOrderDetails()
        {
            try
            {
                if (currentOrderId != Guid.Empty)
                {
                    //Fetch here
                    var orderDetailsResult = client.GetAsync("api/order/orderdetails/" + currentOrderId.ToString()).Result;
                    if (orderDetailsResult.IsSuccessStatusCode)
                    {
                        var orderDetails = JsonConvert.DeserializeObject<OrderDetails>(orderDetailsResult.Content.ReadAsStringAsync().Result);
                        if (orderDetails != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("-------------------------------------------------------");
                            Console.WriteLine("********************Final Bill*************************");
                            Console.WriteLine("ProductName" + Tab + "Quantity" + Tab + "SalesTax" + Tab + "Price");
                            Console.WriteLine("-------------------------------------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            foreach (var product in orderDetails.ProductDisplayList)
                            {
                                Console.WriteLine(string.Format("{0,7}", product.ProductName) + Tab + string.Format("{0,4}", product.Quantity) + Tab + Tab + string.Format("{0,4}", product.SalesTax) + Tab+ Tab + string.Format("{0,5}", product.Price));
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("-------------------------------------------------------");
                            Console.WriteLine("-------------------------------------------------------");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Tab + Tab + Tab + Tab  + string.Format("{0,4}", "Total Tax :") + orderDetails.TotalTax);
                            Console.WriteLine(Tab + Tab + Tab + Tab  + "Total Price :" + orderDetails.TotalPrice);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("**********************Thank You***********************");
                        }
                        else
                        {
                            Console.WriteLine("Fetch Order Details- No Records found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fetch Order Details post error occured");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fetch Order Details Error occured");
            }

        }
        static void DisplayAllProducts()
        {
            var products = client.GetAsync("/api/product").Result;
            if (products.IsSuccessStatusCode)
            {
                lstProducts = JsonConvert.DeserializeObject<List<Product>>(products.Content.ReadAsStringAsync().Result);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("*************Product List***********************");
                Console.WriteLine("ProductId" + Tab + "ProductName" + Tab + "Price" + Tab + "InStock");
                Console.WriteLine("------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var product in lstProducts)
                {
                    Console.WriteLine(string.Format("{0,4}", product.ProductId) + Tab+ Tab + string.Format("{0,7}", product.ProductName) + Tab + string.Format("{0,5}", product.Price) + Tab + string.Format("{0,6}", product.InStock));
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("--------------------------------------------");
            }
            else
            {
                Console.WriteLine("Problem displaying product list");
            }
        }
    }
}
