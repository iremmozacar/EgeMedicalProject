using System;
using EgeApp.Backend.Shared.ComplexTypes;

namespace EgeApp.Backend.Shared.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderState OrderState { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
