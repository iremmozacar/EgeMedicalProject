using System;
using Microsoft.AspNetCore.Identity;

namespace EgeApp.Frontend.Mvc.Data.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
