using System;

namespace EgeApp.Backend.Shared.Dtos.CartDtos;

public class ChangeQuantityDto
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}
