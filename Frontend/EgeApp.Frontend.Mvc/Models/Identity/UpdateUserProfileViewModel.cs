using System;
using System.ComponentModel.DataAnnotations;
using EgeApp.Frontend.Mvc.Data.Entities;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class UpdateUserProfileViewModel
{
    public string UserId { get; set; }

    [Display(Name = "Ad")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    public string FirstName { get; set; }

    [Display(Name = "Soyad")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz email formatı")]
    public string Email { get; set; }

    [Display(Name = "Kullanıcı Adı")]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    public string UserName { get; set; }

    [Display(Name = "Telefon Numarası")]
  
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "En az bir rol seçmelisiniz.")]
    public List<string> UserRoles { get; set; }

    public List<AppRole> Roles { get; set; }
}
