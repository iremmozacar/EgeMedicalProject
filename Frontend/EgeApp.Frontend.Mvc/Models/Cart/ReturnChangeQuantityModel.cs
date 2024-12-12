using System;

namespace EgeApp.Frontend.Mvc.Models.Cart;

public class ReturnChangeQuantityModel
{
    public decimal CartItemTotal { get; set; }
    public decimal SubTotal { get; set; }
    public decimal CartTotal { get; set; }
}
