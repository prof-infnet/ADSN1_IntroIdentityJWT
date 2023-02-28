using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcClient.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            List<Product> productList = new List<Product>();

            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5161/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    productList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }

            return View(productList);
        }



        // GET: ProductsController/Create
        public ViewResult Create() => View();


        // POST: ProductsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            Product addProduct = new Product();

            using(var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                
                using(var response = await httpClient.PostAsync("http://localhost:5161/api/Products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    addProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(addProduct);
        }









        public async Task<ActionResult> Update(int id)
        {
            Product product = new Product();
            
            using (var httpClient =new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5161/api/Products/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Update(Product product)
        {
            Product receivedProduct = new Product();

            using (var httpClient =new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://localhost:5161/api/Products" + product.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(receivedProduct);
        }



















        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
