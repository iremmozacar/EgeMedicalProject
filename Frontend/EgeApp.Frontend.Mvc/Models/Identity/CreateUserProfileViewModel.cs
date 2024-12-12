using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EgeApp.Frontend.Mvc.Data.Entities;

namespace EgeApp.Frontend.Mvc.Models.Identity
{
    public class CreateUserProfileViewModel
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Geçersiz email formatı")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string UserName { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil")]
        public string RePassword { get; set; }

       
        public List<string> UserRoles { get; set; } = new List<string>();

      
        public List<AppRole> Roles { get; set; } = new List<AppRole>();
    }
}
