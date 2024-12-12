using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using EgeApp.Backend.Business.Abstract;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;

namespace EgeApp.Backend.Business.Concrete
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;

        public CartItemService(ICartItemRepository cartItemRepository, ICartRepository cartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
        }

        public async Task<ResponseDto<NoContent>> ChangeQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
            if (cartItem == null)
            {
                return ResponseDto<NoContent>.Fail("İlgili ürün sepette bulunamadı!", StatusCodes.Status404NotFound);
            }
            cartItem.Quantity = quantity;
            await _cartItemRepository.UpdateAsync(cartItem);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<NoContent>> ClearCartAsync(int cartId)
        {
            var cart = await _cartRepository.GetAsync(x => x.Id == cartId, source => source.Include(x => x.CartItems));
            if (cart == null)
            {
                return ResponseDto<NoContent>.Fail("İlgili sepet bulunamadı!", StatusCodes.Status404NotFound);
            }
            foreach (var x in cart.CartItems)
            {
                await _cartItemRepository.DeleteAsync(x);
            }
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<int>> CountAsync(int cartId)
        {
            var count = await _cartItemRepository.GetCountAsync(x => x.CartId == cartId);
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<NoContent>> DeleteCartItemAsync(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetAsync(x => x.Id == cartItemId);
            if (cartItem == null)
            {
                return ResponseDto<NoContent>.Fail("İlgi ürün sepette bulunamadı!", StatusCodes.Status404NotFound);
            }
            await _cartItemRepository.DeleteAsync(cartItem);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CartItem>> GetCartItemAsync(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetAsync(x => x.Id == cartItemId, source => source.Include(x => x.Product));
            if (cartItem == null)
            {
                return ResponseDto<CartItem>.Fail("İlgi ürün sepette bulunamadı!", StatusCodes.Status404NotFound);
            }
            return ResponseDto<CartItem>.Success(cartItem, StatusCodes.Status200OK);
        }
    }
}
