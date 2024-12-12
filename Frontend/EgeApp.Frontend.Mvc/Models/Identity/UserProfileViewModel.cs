using System;
using System.ComponentModel.DataAnnotations;
using EgeApp.Frontend.Mvc.Models.Order;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class UserProfileViewModel
{
    [Display(Name = "Ad")]
    public string FirstName { get; set; }
    [Display(Name = "Soyad")]
    public string LastName { get; set; }
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; set; }
    [Display(Name = "Telefon Numarası")]
    public string PhoneNumber { get; set; }
    [Display(Name = "Geçerli Şifre")]
    public string CurrentPassword { get; set; }
    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; set; }
    [Display(Name = "Yeni Şifre Tekrar")]
    [Compare("NewPassword", ErrorMessage = "Şifreler aynı değil")]
    public string ReNewPassword { get; set; }
    public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

}
