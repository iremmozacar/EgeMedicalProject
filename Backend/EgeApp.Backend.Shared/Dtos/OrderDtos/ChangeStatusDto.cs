using System;
using EgeApp.Backend.Shared.ComplexTypes;

namespace EgeApp.Backend.Shared.Dtos.OrderDtos
{
    public class ChangeStatusDto
    {
        public int Id { get; set; }
        public OrderState OrderState { get; set; }
    }
}
