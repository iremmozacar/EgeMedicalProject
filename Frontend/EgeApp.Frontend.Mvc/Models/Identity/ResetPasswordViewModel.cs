using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class ResetPasswordViewModel
{
    public string UserId { get; set; }
    public string TokenCode { get; set; }

    [DisplayName("Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DisplayName("Parola Tekrar")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "İki şifre birbiriyle uyuşmuyor.")]
    public string RePassword { get; set; }
}
