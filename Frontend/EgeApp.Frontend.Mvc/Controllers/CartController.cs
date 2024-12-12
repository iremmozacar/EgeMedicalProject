using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Models.Cart;
using EgeApp.Frontend.Mvc.Services;
using EgeApp.Backend.Shared.Dtos.CartDtos;
using EgeApp.Frontend.Mvc.Models.Product;


namespace EgeApp.Frontend.Mvc.Controllers
{
    
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;

        public CartController(UserManager<AppUser> userManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CartViewModel cartViewModel;

            if (!User.Identity.IsAuthenticated)
            {
               
                var cartItems = HttpContext.Session.GetString("CartItems");
                var sessionCart = string.IsNullOrEmpty(cartItems)
                    ? new List<CartItemViewModel>()
                    : JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartItems);

                cartViewModel = new CartViewModel
                {
                    Id = 0, 
                    CreatedDate = DateTime.Now,
                    UserId = "Guest",
                    CartItems = sessionCart
                };

                return View(cartViewModel);
            }

           
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);

            var cartResult = await CartService.GetCartAsync(userId);

            if (!cartResult.IsSucceeded)
            {
                _notyfService.Error("Sepet yüklenirken bir hata oluştu.");
                return RedirectToAction("Index", "Home");
            }

            cartViewModel = MapToViewModel(cartResult.Data);
            return View(cartViewModel);
        }



        private CartViewModel MapToViewModel(CartDto cartDto)
        {
            if (cartDto == null) return new CartViewModel();

            return new CartViewModel
            {
                Id = cartDto.Id,
                CreatedDate = cartDto.CreatedDate,
                UserId = cartDto.UserId,
                CartItems = cartDto.CartItems.Select(item => new CartItemViewModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Product = new ProductViewModel
                    {
                        Name = item.Product?.Name ?? item.ProductName,
                        ImageUrl = item.Product?.ImageUrl ?? item.ImageUrl,
                    },
                    Price = item.Price,
                    Quantity = item.Quantity,
                    CreatedDate = item.CreatedDate
                }).ToList()
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                
                var cartItems = HttpContext.Session.GetString("CartItems");
                List<AddToCartViewModel> sessionCart;

                if (!string.IsNullOrEmpty(cartItems))
                {
                    sessionCart = JsonConvert.DeserializeObject<List<AddToCartViewModel>>(cartItems);
                }
                else
                {
                    sessionCart = new List<AddToCartViewModel>();
                }

               
                var existingItem = sessionCart.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += model.Quantity;
                }
                else
                {
                   
                    var productDetails = await CartService.GetProductDetailsAsync(model.ProductId);
                    sessionCart.Add(new AddToCartViewModel
                    {
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        ProductName = productDetails.Name,
                        ImageUrl = productDetails.ImageUrl,
                        Price = productDetails.Price
                    });
                }

               
                HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(sessionCart));
                _notyfService.Success("Ürün sepete eklendi!");
                return RedirectToAction("Index");
            }

           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.UserId = await _userManager.GetUserIdAsync(user);

        
            var result = await CartService.AddToCartAsync(model);

      
            if (!result.IsSucceeded)
            {
                _notyfService.Error("Ürün sepete eklenirken bir hata oluştu.");
                return RedirectToAction("Index");
            }

            _notyfService.Success("Ürün başarıyla sepete eklendi!");
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
              
                var cartItems = HttpContext.Session.GetString("CartItems");
                if (string.IsNullOrEmpty(cartItems))
                {
                    return BadRequest("Sepet boş.");
                }

                var sessionCart = JsonConvert.DeserializeObject<List<AddToCartViewModel>>(cartItems);

               
                var itemToRemove = sessionCart.FirstOrDefault(x => x.ProductId == id);
                if (itemToRemove != null)
                {
                    sessionCart.Remove(itemToRemove);
                    HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(sessionCart));
                    _notyfService.Success("Ürün sepetinizden kaldırıldı!");
                }
                else
                {
                    return BadRequest("Ürün sepetinizde bulunamadı.");
                }

                return RedirectToAction("Index");
            }

        
            var result = CartService.DeleteCartItemAsync(id).Result;
            if (!result.IsSucceeded)
            {
                _notyfService.Error("Ürün sepetten kaldırılırken bir hata oluştu.");
            }
            else
            {
                _notyfService.Success("Ürün sepetinizden başarıyla kaldırıldı!");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TransferCartItemsToUser()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = await _userManager.GetUserIdAsync(user);

            var cartItems = HttpContext.Session.GetString("CartItems");
            if (string.IsNullOrEmpty(cartItems)) return RedirectToAction("Index");

            var sessionCart = JsonConvert.DeserializeObject<List<AddToCartViewModel>>(cartItems);

            foreach (var item in sessionCart)
            {
                item.UserId = userId;
                var result = await CartService.AddToCartAsync(item);
                if (!result.IsSucceeded)
                {
                    _notyfService.Error($"Ürün ({item.ProductId}) sepete eklenirken bir hata oluştu.");
                }
            }

          
            HttpContext.Session.Remove("CartItems");
            _notyfService.Success("Geçici sepetiniz hesabınıza aktarıldı!");
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int cartItemId, int quantity)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = await _userManager.GetUserIdAsync(user);
            var result = await CartService.ChangeQuantityAsync(cartItemId, quantity, userId);
            if (!result.IsSucceeded)
            {
                _notyfService.Error("Bir hata oluştu");
                return RedirectToAction("Index");
            }
            return Json(result.Data);
        }

        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var result = await CartService.DeleteCartItemAsync(id);
            if (!result.IsSucceeded)
            {
                _notyfService.Error("Bir hata oluştu");
            }
            _notyfService.Success("Ürün sepetinizden başarıyla kaldırıldı");
            return RedirectToAction("Index");

        }

        [HttpDelete]
        public IActionResult ClearCart(int cartId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                
                HttpContext.Session.Remove("CartItems");
                _notyfService.Success("Sepet başarıyla temizlendi.");
                return Ok("Sepet başarıyla temizlendi.");
            }

           
            var result = CartService.ClearCartAsync(cartId).Result; 
            if (!result.IsSucceeded)
            {
                return BadRequest(result.Error ?? "Sepet temizlenemedi.");
            }

            return Ok("Sepet başarıyla temizlendi.");
        }




    }
}


