using System.Collections.Generic;
using EgeApp.Frontend.Mvc.Models.Category;

namespace EgeApp.Frontend.Mvc.Models.Product
{
    public class ProductsCategoriesViewModel
    {
        public List<CategoryViewModel> CategoryList { get; set; } 
        public List<ProductViewModel> ProductList { get; set; } 
        public List<ProductViewModel> DiscountedProducts { get; set; } 
        public List<ProductViewModel> BestSellers { get; set; } 
    }
}
