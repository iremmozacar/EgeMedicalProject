using System;

namespace EgeApp.Backend.Shared.Dtos.OrderDtos
{
    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
