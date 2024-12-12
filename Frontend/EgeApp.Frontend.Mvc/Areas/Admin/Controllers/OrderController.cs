using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Services;

namespace EgeApp.Frontend.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin, Admin")]
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public OrderController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? id = null)
        {
            var orderStates = new List<SelectListItem>(){
                new(){ Text="Tümü", Value="", Selected=id==null},
                new(){ Text="Sipariş Alındı", Value="0", Selected=id==0},
                new(){ Text="Hazırlanıyor", Value="1", Selected=id==1},
                new(){ Text="Gönderildi", Value="2", Selected=id==2},
                new(){ Text="Teslim Edildi", Value="3", Selected=id==3}
            };
            ViewBag.OrderStates = orderStates;
            var orderState = id;
            var result = id == null ? await OrderService.GetOrdersAsync() : await OrderService.GetOrdersByOrderStateAsync((int)orderState);

            if (!result.IsSucceeded)
            {
                ViewBag.Error = result.Error;
            }

            if (result.Data != null)
            {
                for (int i = 0; i < result.Data.Count; i++)
                {
                    result.Data[i].User = await _userManager.FindByIdAsync(result.Data[i].UserId);
                }
            }

            ViewBag.SelectedOrderState = orderState;
            return View(result.Data);


        }

    }
}
