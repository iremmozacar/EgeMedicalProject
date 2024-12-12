using System;
using System.Text.Json.Serialization;

namespace EgeApp.Frontend.Mvc.Models.Cart;

public class AddToCartViewModel
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; } = 1;

    [JsonPropertyName("userId")]
    public string UserId { get; set; }
    public string ProductName { get; set; } 
    public string ImageUrl { get; set; } 
    public decimal Price { get; set; } 
    
}
