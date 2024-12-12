using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using EgeApp.Frontend.Mvc.Models.Category;

namespace EgeApp.Frontend.Mvc.Models.Product
{
    public class ProductViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Ürün adı gereklidir.")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Fiyat gereklidir.")]
        public decimal Price { get; set; }

        [JsonPropertyName("discountedPrice")]
        public decimal? DiscountedPrice { get; set; }

        [JsonPropertyName("salesCount")]
        public int SalesCount { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [Display(Name = "Ürün Resmi")]
        public IFormFile Image { get; set; } 

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

        [JsonPropertyName("categoryId")]
        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        public int CategoryId { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("isHome")]
        public bool IsHome { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [JsonPropertyName("category")]
        public CategoryViewModel Category { get; set; }

        [JsonPropertyName("categoryList")]
        public IEnumerable<CategoryViewModel>? CategoryList { get; set; }
    }
}