using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Backend.Business.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.ComplexTypes;
using EgeApp.Backend.Shared.Dtos.OrderDtos;
using EgeApp.Backend.Shared.Helpers;

namespace EgeApp.Backend.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateDto orderCreateDto)
        {
            var response = await _orderService.CreateAsync(orderCreateDto);
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var response = await _orderService.GetOrderAsync(id);
            return CreateActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _orderService.GetOrdersAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var response = await _orderService.GetOrdersAsync(userId);
            return CreateActionResult(response);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetOrdersByProductId(int productId)
        {
            var response = await _orderService.GetOrdersAsync(productId);
            return CreateActionResult(response);
        }

        [HttpGet("{orderState}")]
        public async Task<IActionResult> GetOrdersByOrderState(OrderState orderState)
        {
            var response = await _orderService.GetOrdersByOrderStateAsync(orderState);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var response = await _orderService.CancelOrderAsync(id);
            return CreateActionResult(response);
        }



        [HttpPut]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto changeStatusDto)
        {
            var response = await _orderService.ChangeOrderStatusAsync(changeStatusDto.Id, changeStatusDto.OrderState);
            return CreateActionResult(response);
        }
    }
}
