using System;
using System.Text.Json.Serialization;

namespace EgeApp.Backend.Shared.Dtos.CartDtos;

public class AddToCartDto
{
    public int ProductId { get; set; }

    public int Quantity { get; set; } = 1;
    public string UserId { get; set; }

    public string ProductName { get; set; } 
    public string ImageUrl { get; set; } 
    public decimal Price { get; set; } 

}
