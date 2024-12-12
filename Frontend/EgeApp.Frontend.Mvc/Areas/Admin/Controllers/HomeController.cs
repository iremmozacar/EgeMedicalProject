using System.Net.Http;
using System.Text.Json;
using EgeApp.Frontend.Mvc.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgeApp.Frontend.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin, Admin")]
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(); 
        }
        public ActionResult Index()
        {
            return View();
        }

        
            public IActionResult Error()
            {
                return View();
            }
        

         public async Task<IActionResult> GetHomes()
        {
            var apiUrl = "http://localhost:5200/api/Products/GetHomes/true";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductViewModel>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(products);
            }
            else
            {
                ViewData["ErrorMessage"] = "API'dan veriler alınamadı.";
                return View(new List<ProductViewModel>());
            }

        }

    }
}