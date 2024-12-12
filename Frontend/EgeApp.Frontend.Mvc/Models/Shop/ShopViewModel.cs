using System;
using EgeApp.Frontend.Mvc.Models.Category;
using EgeApp.Frontend.Mvc.Models.Product;
using EgeApp.Frontend.Mvc.Models.Shop;

namespace EgeApp.Frontend.Mvc.Models.Other;

public class ShopViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public List<ProductViewModel> Products { get; set; }

    public PaginationViewModel Pagination { get; set; }
}