
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcClient.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            List<Product> productList = new List<Product>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Products"))
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

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                
                using(var response = await httpClient.PostAsync("https://localhost:5001/api/Products", content))
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

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient =new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Products" + id))
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

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient =new HttpClient())
            {

                

                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.PutAsync("https://localhost:5001/api/Products" + product.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(receivedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Products" + productId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

    }
}