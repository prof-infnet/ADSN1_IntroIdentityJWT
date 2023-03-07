using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcClient.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDetails userDetails)
        {
            using (var httpClient = new HttpClient())
            {

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Register", stringContent);
            }

            return Redirect("~/Auth/Login");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials loginCredentials)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginCredentials), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Auth/Login", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();

                    if (token == "Invalid Credentials")
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                        return Redirect("~/Auth/Login");
                    }

                    HttpContext.Session.SetString("JWToken", token);

                }

            }

            return Redirect("~/Products/Index");


        }


    }
}
