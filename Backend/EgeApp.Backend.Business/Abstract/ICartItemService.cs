using System;
using Microsoft.AspNetCore.Http.HttpResults;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;

namespace EgeApp.Backend.Business.Abstract
{
    public interface ICartItemService
    {
        Task<ResponseDto<NoContent>> ChangeQuantityAsync(int cartItemId, int quantity);
        Task<ResponseDto<int>> CountAsync(int cartId);
        Task<ResponseDto<NoContent>> DeleteCartItemAsync(int cartItemId);
        Task<ResponseDto<NoContent>> ClearCartAsync(int cartId);
        Task<ResponseDto<CartItem>> GetCartItemAsync(int cartItemId);
    }
}
