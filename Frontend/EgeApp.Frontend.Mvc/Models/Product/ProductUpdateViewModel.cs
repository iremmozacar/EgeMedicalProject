using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgeApp.Frontend.Mvc.Models.Product;

public class ProductUpdateViewModel
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discountedPrice")]
    public decimal? DiscountedPrice { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("isDiscounted")]
    public bool IsDiscounted { get; set; }

    [JsonPropertyName("isFreeShipping")]
    public bool? IsFreeShipping { get; set; }

    [JsonPropertyName("isSpecialProduct")]
    public bool? IsSpecialProduct { get; set; }

    [JsonPropertyName("isSameDayShipping")]
    public bool? IsSameDayShipping { get; set; }

    [JsonPropertyName("CategoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("isHome")]
    public bool IsHome { get; set; }

   

    [JsonIgnore]
    public IFormFile Image { get; set; }

  

    [JsonPropertyName("categoryList")]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}
