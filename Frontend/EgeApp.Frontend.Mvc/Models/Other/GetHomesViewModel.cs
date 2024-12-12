using System;
using System.Text.Json.Serialization;
using EgeApp.Frontend.Mvc.Models.Category;

namespace EgeApp.Frontend.Mvc.Models.Other;

public class GetHomesViewModel
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

    [JsonPropertyName("salesCount")]
    public int SalesCount { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("isDiscounted")]
    public bool IsDiscounted { get; set; }

    [JsonPropertyName("isFreeShipping")]
    public bool IsFreeShipping { get; set; }

    [JsonPropertyName("isSpecialProduct")]
    public bool IsSpecialProduct { get; set; }

    [JsonPropertyName("isSameDayShipping")]
    public bool IsSameDayShipping { get; set; }

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("isHome")]
    public bool IsHome { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("modifiedDate")]
    public DateTime ModifiedDate { get; set; }

    [JsonPropertyName("category")]
    public CategoryViewModel Category { get; set; }

    [JsonPropertyName("categoryList")]
    public object CategoryList { get; set; }
}
