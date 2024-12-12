using System;

namespace EgeApp.Frontend.Mvc.Models.Product;

public class ProductListViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public bool IsActive { get; set; }
    public bool? IsFreeShipping { get; set; }
}
