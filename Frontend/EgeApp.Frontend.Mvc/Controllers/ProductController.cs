using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Services;
using EgeApp.Frontend.Mvc.Models.Product;

namespace EgeApp.Frontend.Mvc.Controllers
{

    public class ProductController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;

        public ProductController(INotyfService notyfService, UserManager<AppUser> userManager)
        {
            _notyfService = notyfService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await ProductService.GetAllAsync();
            if (!result.IsSucceeded)
            {
                TempData["Error"] = result.Error ?? "Ürünler yüklenirken bir hata oluştu.";
                return RedirectToAction("Error", "Home");
            }

            return View(result.Data);
        }
        public IActionResult Shop()
        {
            return View(); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await ProductService.GetByIdAsync(id);

            if (!result.IsSucceeded)
            {
                _notyfService.Error("Ürün detayları yüklenemedi.");
                return RedirectToAction("Index");
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var cartResult = await CartService.GetCartAsync(user.Id);

                if (!cartResult.IsSucceeded)
                {
                    _notyfService.Error("Sepet bilgileri alınırken bir hata oluştu.");
                    return RedirectToAction("Index");
                }

                ViewBag.CountOfItems = cartResult.Data.CountOfItem;
            }

            return View(result.Data);
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

        public async Task<IActionResult> ProductList()
        {
            var response = await ProductService.GetAllAsync();

         
            if (response != null && response.Data != null)
            {
                var products = response.Data
                    .Select(p => new ProductListViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        Price = p.Price,
                        DiscountedPrice = p.DiscountedPrice,
                        IsActive = p.IsActive,
                        IsFreeShipping = p.IsFreeShipping
                    }).ToList();

                return View(products);
            }

      
            return View(new List<ProductListViewModel>());
        }


    }
}
