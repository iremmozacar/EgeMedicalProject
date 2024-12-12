using System;

namespace EgeApp.Backend.Entity.Concrete
{
    public class Cart
    {
        public int Id { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
        public string UserId { get; set; } 
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
