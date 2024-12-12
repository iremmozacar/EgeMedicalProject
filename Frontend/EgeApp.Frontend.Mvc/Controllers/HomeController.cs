using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Models.Product;
using EgeApp.Frontend.Mvc.Services;

namespace EgeApp.Frontend.Mvc.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                
                var resultCategories = await CategoryService.GetActives();
                if (resultCategories == null || !resultCategories.IsSucceeded)
                {
                    TempData["Error"] = resultCategories?.Error ?? "An error occurred while fetching categories.";
                    return RedirectToAction("Error", "Home");
                }

           
                var resultProducts = await ProductService.GetHomesAsync();
                if (resultProducts == null || !resultProducts.IsSucceeded)
                {
                    TempData["Error"] = resultProducts?.Error ?? "An error occurred while fetching products.";
                    return RedirectToAction("Error", "Home");
                }

              
                var discountedProducts = await ProductService.GetDiscountedProductsAsync();
                if (discountedProducts == null || !discountedProducts.IsSucceeded)
                {
                    TempData["Error"] = discountedProducts?.Error ?? "An error occurred while fetching discounted products.";
                    return RedirectToAction("Error", "Home");
                }

                var bestSellers = await ProductService.GetBestSellersAsync(10);
                if (bestSellers == null || !bestSellers.IsSucceeded)
                {
                    TempData["Error"] = bestSellers?.Error ?? "An error occurred while fetching best sellers.";
                    return RedirectToAction("Error", "Home");
                }

             
                ProductsCategoriesViewModel model = new()
                {
                    CategoryList = resultCategories.Data,
                    ProductList = resultProducts.Data,
                    DiscountedProducts = discountedProducts.Data ?? new List<ProductViewModel>(),
                    BestSellers = bestSellers.Data ?? new List<ProductViewModel>()
                };

              
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (user != null)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        var cartResult = await CartService.GetCartAsync(userId);

                      
                        ViewBag.CountOfItems = cartResult?.IsSucceeded == true ? cartResult.Data.CountOfItem : 0;
                    }
                    else
                    {
                        ViewBag.CountOfItems = 0;
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
               
                TempData["Error"] = $"An exception occurred: {ex.Message}";
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet("GetDiscountedProducts")]
        public async Task<IActionResult> GetDiscountedProducts()
        {
            var discountedProducts = await ProductService.GetDiscountedProductsAsync();

            if (discountedProducts == null || !discountedProducts.IsSucceeded || discountedProducts.Data == null || !discountedProducts.Data.Any())
            {
                return NotFound("No discounted products found.");
            }

            return Ok(discountedProducts.Data);
        }
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(); 
        }
        public IActionResult Information()
        {
            return View();
        }
    }
}
