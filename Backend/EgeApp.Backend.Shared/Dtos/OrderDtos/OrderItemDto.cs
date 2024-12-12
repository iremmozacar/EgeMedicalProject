using System;
using System.Drawing;
using EgeApp.Backend.Shared.Dtos.ProductDtos;

namespace EgeApp.Backend.Shared.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public InOrderItemOrderDto Order { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
