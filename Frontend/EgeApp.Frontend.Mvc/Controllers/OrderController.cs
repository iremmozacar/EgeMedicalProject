using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Models.Order;
using EgeApp.Frontend.Mvc.Services;
using EgeApp.Frontend.Mvc.Models.Cart;
using EgeApp.Backend.Shared.Dtos.CartDtos;
using EgeApp.Frontend.Mvc.Models.Product;

namespace EgeApp.Frontend.Mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;

        public OrderController(UserManager<AppUser> userManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = await _userManager.GetUserIdAsync(user);
            var cartResult = await CartService.GetCartAsync(userId);

            if (!cartResult.IsSucceeded)
            {
                return RedirectToAction("Error", "Home");
            }

            var cartViewModel = MapToViewModel(cartResult.Data);

            OrderCreateViewModel model = new()
            {
                Cart = cartViewModel,
                FirstName = "Sezen",
                LastName = "Aksu",
                Address = "Düpedüz Sk. Yamuk yumuk ap. D:12 Beşiktaş",
                City = "İstanbul",
                Email = "sezenaksu@gmail.com",
                PhoneNumber = "5557778844",
                PaymentType = 0,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCreateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userId = await _userManager.GetUserIdAsync(user);
            var cartResult = await CartService.GetCartAsync(userId);

            if (!cartResult.IsSucceeded)
            {
                return RedirectToAction("Error", "Home");
            }

        
            var cartViewModel = MapToViewModel(cartResult.Data);

            List<OrderItemViewModel> orderItems = new();
            foreach (var cartItem in cartResult.Data.CartItems)
            {
                orderItems.Add(new OrderItemViewModel
                {
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity
                });
            }

            model.OrderItems = orderItems;
            model.UserId = userId;

            var result = await OrderService.AddOrderAsync(model);
            if (!result.IsSucceeded)
            {
                _notyfService.Error(result.Error);
                return View(model);
            }

            await CartService.ClearCartAsync(cartResult.Data.Id);
            _notyfService.Success("Ödeme işlemi başarıyla tamamlandı!");
            _notyfService.Information("Siparişiniz alındı!");
            return RedirectToAction("Index", "Home");
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
                        Name = item.Product?.Name,
                        ImageUrl = item.Product?.ImageUrl
                    },
                    Price = item.Price,
                    Quantity = item.Quantity,
                    CartId = item.CartId
                }).ToList()
            };
        }
    }
    }
