using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EgeApp.Frontend.Mvc.Models.Cart;

namespace EgeApp.Frontend.Mvc.Models.Order;

public class OrderCreateViewModel
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("firstName")]
    [Display(Name = "Ad")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    [Display(Name = "Soyad")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    [Display(Name = "Adres")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    public string Address { get; set; }

    [JsonPropertyName("city")]
    [Display(Name = "Şehir")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    public string City { get; set; }

    [JsonPropertyName("phoneNumber")]
    [Display(Name = "Telefon No")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Bu alanı boş bırakmayınız!")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir email giriniz!")]
    public string Email { get; set; }

    [JsonPropertyName("paymentType")]
    [Display(Name = "Ödeme Tipi")]
    public int PaymentType { get; set; }

    [JsonPropertyName("orderItems")]
    public List<OrderItemViewModel> OrderItems { get; set; }

    public CartViewModel Cart { get; set; }
}
