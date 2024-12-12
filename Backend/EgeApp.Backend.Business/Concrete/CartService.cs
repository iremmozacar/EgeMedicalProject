using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using EgeApp.Backend.Business.Abstract;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.Dtos.CartDtos;
using EgeApp.Backend.Shared.Dtos;
using EgeApp.Backend.Shared.Dtos.ProductDtos;

namespace EgeApp.Backend.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ResponseDto<NoContent>> AddToCartAsync(string userId, int productId, int quantity)
        {
            Cart cart = await _cartRepository.GetAsync(x => x.UserId == userId, source => source.Include(x => x.CartItems));
            if (cart == null)
            {
                return ResponseDto<NoContent>.Fail("Kullanıcıya ait bir sepet bulunamadı!", StatusCodes.Status404NotFound);
            }
            
            var index = cart.CartItems.FindIndex(x => x.ProductId == productId);
            if (index < 0)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id
                });
            }
            else
            {
                cart.CartItems[index].Quantity = quantity;
            }
            await _cartRepository.UpdateAsync(cart);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }



        public async Task<ResponseDto<NoContent>> InitilaizeCartAsync(string userId)
        {
            await _cartRepository.CreateAsync(new Cart { UserId = userId });
            return ResponseDto<NoContent>.Success(StatusCodes.Status201Created);
        }
        public async Task<ResponseDto<CartDto>> GetCartByUserIdAsync(string? userId)
        {
            Cart cart;

          
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _cartRepository.GetCartAsync(userId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CreatedDate = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.CreateAsync(cart);
                }
            }
            else
            {
                
                cart = new Cart
                {
                    UserId = null, 
                    CreatedDate = DateTime.Now,
                    CartItems = new List<CartItem>()
                };
            }

            var cartDto = new CartDto
            {
                Id = cart.Id,
                CreatedDate = cart.CreatedDate,
                UserId = cart.UserId,
                CartItems = cart.CartItems?.Select(item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Product = new ProductDto
                    {
                        Name = item.Product.Name,
                        ImageUrl = item.Product.ImageUrl,
                        
                    }
                }).ToList() ?? new List<CartItemDto>()
            };
          
            return ResponseDto<CartDto>.Success(cartDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CartDto>> GetCartBySessionOrUserIdAsync(HttpContext httpContext, string? userId)
        {
            Cart cart;

            if (!string.IsNullOrEmpty(userId))
            {
   
                cart = await _cartRepository.GetCartAsync(userId);

               
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CreatedDate = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.CreateAsync(cart);
                }
            }
            else
            {
               
                var sessionId = httpContext.Session.GetString("CartSessionId");

                if (!string.IsNullOrEmpty(sessionId))
                {
                    cart = await _cartRepository.GetCartAsync(sessionId);
                }
                else
                {
                   
                    cart = new Cart
                    {
                        UserId = null,
                        CreatedDate = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.CreateAsync(cart);

                  
                    httpContext.Session.SetString("CartSessionId", cart.Id.ToString());
                }
            }

            var cartDto = new CartDto
            {
                Id = cart.Id,
                CreatedDate = cart.CreatedDate,
                UserId = cart.UserId,
                CartItems = cart.CartItems?.Select(item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Product = new ProductDto
                    {
                        Name = item.Product.Name,
                        ImageUrl = item.Product.ImageUrl,
                       
                    }
                }).ToList() ?? new List<CartItemDto>()
            };

            return ResponseDto<CartDto>.Success(cartDto, StatusCodes.Status200OK);
        }


        public async Task AssignAnonymousCartToUser(HttpContext httpContext, string userId)
        {
           
            var sessionId = httpContext.Session.GetString("CartSessionId");
            if (string.IsNullOrEmpty(sessionId)) return;

            var cart = await _cartRepository.GetCartAsync(sessionId);
            if (cart == null) return;

      
            if (!string.IsNullOrEmpty(cart.UserId)) return;

           
            cart.UserId = userId;
            await _cartRepository.UpdateAsync(cart);

            httpContext.Session.Remove("CartSessionId");
        }
    }
    
}
