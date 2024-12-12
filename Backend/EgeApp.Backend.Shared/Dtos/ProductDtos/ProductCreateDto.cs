using System.ComponentModel.DataAnnotations;
using EgeApp.Backend.Shared.Dtos.CategoryDtos;

namespace EgeApp.Backend.Shared.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }  
        public string Description { get; set; }  
        [Required]
        public decimal Price { get; set; }  
        public decimal? DiscountedPrice { get; set; }  
        public string ImageUrl { get; set; }  
        public bool IsActive { get; set; } = true; 
        public string? WarrantyPeriod { get; set; } 
        public bool IsDiscounted { get; set; } 
        public bool? IsFreeShipping { get; set; }  
        public bool? IsSpecialProduct { get; set; } 
        public bool? IsSameDayShipping { get; set; }  
        public bool? IsLimitedStock { get; set; } 
        public string? Url { get; set; } 
        public string? Brand { get; set; }  
        public bool IsHome { get; set; }  
        [Required]
        public int CategoryId { get; set; }  
        public IEnumerable<CategoryDto>? Categories { get; set; }  
    }

}