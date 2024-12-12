using System;
using Microsoft.AspNetCore.Http.HttpResults;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.ComplexTypes;
using EgeApp.Backend.Shared.Dtos.OrderDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;

namespace EgeApp.Backend.Business.Abstract
{
    public interface IOrderService
    {
        Task<ResponseDto<NoContent>> CreateAsync(OrderCreateDto orderCreateDto);
        Task<ResponseDto<List<OrderDto>>> GetOrdersAsync();
        Task<ResponseDto<List<OrderDto>>> GetOrdersAsync(string userId);
        Task<ResponseDto<List<OrderDto>>> GetOrdersAsync(int productId);
        Task<ResponseDto<List<OrderDto>>> GetOrdersByOrderStateAsync(OrderState orderState);
        Task<ResponseDto<OrderDto>> GetOrderAsync(int id);
        Task<ResponseDto<NoContent>> ChangeOrderStatusAsync(int id, OrderState orderState);
        Task<ResponseDto<NoContent>> CancelOrderAsync(int id);
    }
}
