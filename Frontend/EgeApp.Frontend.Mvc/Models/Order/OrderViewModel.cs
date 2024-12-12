using System;
using System.Text.Json.Serialization;
using EgeApp.Frontend.Mvc.Data.Entities;

namespace EgeApp.Frontend.Mvc.Models.Order;

public class OrderViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("orderDate")]
    public DateTime OrderDate { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("paymentType")]
    public int PaymentType { get; set; }

    [JsonPropertyName("orderState")]
    public int OrderState { get; set; }
    [JsonPropertyName("orderItems")]
    public List<OrderItemViewModel> OrderItems { get; set; }
    public decimal GetTotal()
    {
        return OrderItems.Sum(x => x.Quantity * x.Price);
    }
    public AppUser User { get; set; } = null;
}
