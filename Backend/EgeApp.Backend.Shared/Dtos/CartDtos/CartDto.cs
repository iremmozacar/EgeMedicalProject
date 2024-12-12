using System;

namespace EgeApp.Backend.Shared.Dtos.CartDtos;

public class CartDto
{
    public int Id { get; set; } 
    public DateTime CreatedDate { get; set; } 
    public string UserId { get; set; } 
    public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>(); 

    
    public int CountOfItem => CartItems?.Count ?? 0; 
        public decimal GetTotalPrice()
    {
        return CartItems?.Sum(x => x.Price * x.Quantity) ?? 0; 
    }
}