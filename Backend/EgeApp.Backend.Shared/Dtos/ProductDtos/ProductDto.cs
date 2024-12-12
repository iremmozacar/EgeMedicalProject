using System;
using System.Collections.Generic;
using EgeApp.Backend.Shared.Dtos.CategoryDtos;

namespace EgeApp.Backend.Shared.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal Price { get; set; } 
        public decimal? DiscountedPrice { get; set; } 
        public int SalesCount { get; set; } 
        public string ImageUrl { get; set; } 
        public bool IsActive { get; set; } 
        public bool IsDiscounted { get; set; } 
        public bool? IsFreeShipping { get; set; } 
        public bool? IsSpecialProduct { get; set; } 
        public bool? IsSameDayShipping { get; set; } 
        public int CategoryId { get; set; } 
        public string? Url { get; set; } 
        public string? Brand { get; set; } 
        public bool IsHome { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public DateTime ModifiedDate { get; set; } 
        public CategoryDto Category { get; set; } 
        public IEnumerable<CategoryDto> CategoryList { get; set; } 
    }
}
