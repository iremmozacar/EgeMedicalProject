using System;
using System.ComponentModel.DataAnnotations;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class ForgotPasswordViewModel
{
    [Display(Name = "Email adresinizi giriniz")]
    [Required(ErrorMessage = "Boş bırakmayınız")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz email adresi")]
    public string Email { get; set; }
}
