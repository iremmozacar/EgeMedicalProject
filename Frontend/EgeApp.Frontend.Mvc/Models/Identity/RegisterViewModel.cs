using System;
using System.ComponentModel.DataAnnotations;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class RegisterViewModel
{
    [Display(Name = "Ad")]
    [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
    [MinLength(3, ErrorMessage = "Ad en az 3 karakter olmalı.")]
    [MaxLength(20, ErrorMessage = "Ad en fazla 20 karakter olmalı.")]
    [RegularExpression(@"^[a-zA-ZğüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Ad yalnızca harflerden oluşabilir.")]
    public string FirstName { get; set; }

    [Display(Name = "Soyad")]
    [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
    [MinLength(3, ErrorMessage = "Soyad en az 3 karakter olmalı.")]
    [MaxLength(20, ErrorMessage = "Soyad en fazla 20 karakter olmalı.")]
    [RegularExpression(@"^[a-zA-ZğüşöçİĞÜŞÖÇ\s]*$", ErrorMessage = "Soyad yalnızca harflerden oluşabilir.")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; }

    [Display(Name = "Kullanıcı Adı")]
    [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
    [MinLength(3, ErrorMessage = "Kullanıcı adı en az 3 karakter olmalı.")]
    [MaxLength(15, ErrorMessage = "Kullanıcı adı en fazla 15 karakter olmalı.")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Kullanıcı adı yalnızca harf ve rakamlardan oluşabilir.")]
    public string UserName { get; set; }

    [Display(Name = "Parola")]
    [Required(ErrorMessage = "Parola alanı boş bırakılamaz.")]
    [MinLength(8, ErrorMessage = "Parola en az 8 karakter olmalı.")]
    [MaxLength(15, ErrorMessage = "Parola en fazla 15 karakter olmalı.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Parola Tekrar")]
    [Required(ErrorMessage = "Parola tekrar alanı boş bırakılamaz.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Parola aynı değil.")]
    public string RePassword { get; set; }

    [Display(Name = "Telefon Numarası")]
    [Phone(ErrorMessage = "Geçersiz telefon numarası formatı.")]
    [RegularExpression(@"^(\+90|0)?[5][0-9]{9}$", ErrorMessage = "Telefon numarası geçerli bir Türkçe formatında olmalıdır.")]
    public string PhoneNumber { get; set; }
}
