using System;
using System.Text.Json.Serialization;
using EgeApp.Frontend.Mvc.Models.Product;

namespace EgeApp.Frontend.Mvc.Models.Cart;

public class CartItemViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("productId")]
    public int ProductId { get; set; }



    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("product")]
    public ProductViewModel Product { get; set; }

    [JsonPropertyName("cartId")]
    public int CartId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }


    [JsonPropertyName("productName")]
    public string ProductName { get; set; } 

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}
