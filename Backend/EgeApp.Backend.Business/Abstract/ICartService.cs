using System;
using Microsoft.AspNetCore.Http.HttpResults;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.Dtos.CartDtos;
using Microsoft.AspNetCore.Http;

namespace EgeApp.Backend.Business.Abstract
{
    public interface ICartService
    {
        Task<ResponseDto<NoContent>> InitilaizeCartAsync(string userId);
        Task<ResponseDto<CartDto>> GetCartByUserIdAsync(string? userId);
        Task<ResponseDto<NoContent>> AddToCartAsync(string userId, int productId, int quantity);

        Task AssignAnonymousCartToUser(HttpContext httpContext, string userId);
    }
}
