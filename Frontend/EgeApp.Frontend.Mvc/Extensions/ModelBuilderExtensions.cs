using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EgeApp.Frontend.Mvc.Data.Entities;

namespace EgeApp.Frontend.Mvc.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedIdentityData(this ModelBuilder modelBuilder)
    {
        #region Örnek Rol Bilgileri Tanımlanıyor
        List<AppRole> roles = new List<AppRole>
        {
            new() { Name="Super Admin", Description="Sistemdeki her türlü işi yapmaya yetkili rol", NormalizedName="SUPER ADMIN" },
            new() { Name="Admin", Description="Sistemdeki yönetimsel işleri yapmaya yetkili rol", NormalizedName="ADMIN" },
            new() { Name="Customer", Description="Müşterilerin rolü", NormalizedName="CUSTOMER" }
        };
        modelBuilder.Entity<AppRole>().HasData(roles);
        #endregion

        #region Örnek Kullanıcı Bilgileri Tanımlanıyor
        List<AppUser> users = new List<AppUser>
        {
            new() { Id="1", FirstName="Muhammed", LastName="Topçu", Email="denizcoban@example.com", UserName="muhammedtopcu", EmailConfirmed=true, NormalizedEmail="MUHAMMEDTOPCU@EXAMPLE.COM", NormalizedUserName="MUHAMMEDTOPCU" },
            new() { Id="2", FirstName="İrem", LastName="Özacar", Email="iremozacaar@gmail.com", UserName="iremozacar", EmailConfirmed=true, NormalizedEmail="IREMOZACAAR@EXAMPLE.COM", NormalizedUserName="IREMOZACAR" },
            new() { Id="3", FirstName="Fatmanur", LastName="Altın", Email="fatmanuraltin@example.com", UserName="fatmanuraltin", EmailConfirmed=true, NormalizedEmail="FATMANURALTIN@EXAMPLE.COM", NormalizedUserName="FATMANURALTIN" }
        };

        modelBuilder.Entity<AppUser>().HasData(users);
        #endregion

        #region Rol Atama İşlemleri Yapılıyor
        List<IdentityUserRole<string>> identityUserRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { RoleId = roles[0].Id, UserId = users[0].Id },
            new IdentityUserRole<string> { RoleId = roles[1].Id, UserId = users[1].Id },
            new IdentityUserRole<string> { RoleId = roles[2].Id, UserId = users[2].Id }
        };
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
        #endregion

        #region Şifre Atama İşlemleri Yapılıyor
        var passwordHasher = new PasswordHasher<AppUser>();
        users[0].PasswordHash = passwordHasher.HashPassword(users[0], "qwe123.");
        users[1].PasswordHash = passwordHasher.HashPassword(users[1], "qwe123.");
        users[2].PasswordHash = passwordHasher.HashPassword(users[2], "qwe123.");
        #endregion
    }
}