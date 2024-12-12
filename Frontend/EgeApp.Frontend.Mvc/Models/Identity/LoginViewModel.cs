using System.Collections.Generic;
using EgeApp.Frontend.Mvc.Models.Category;
using EgeApp.Frontend.Mvc.Models.Product;

namespace EgeApp.Frontend.Mvc.Models.Identity
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public List<ProductViewModel> ProductList { get; set; }
        public List<CategoryViewModel> CategoryList { get; set; }
    }
}