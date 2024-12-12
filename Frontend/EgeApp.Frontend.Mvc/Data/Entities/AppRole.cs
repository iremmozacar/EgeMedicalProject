using System;
using Microsoft.AspNetCore.Identity;

namespace EgeApp.Frontend.Mvc.Data.Entities;

public class AppRole : IdentityRole
{
    public string Description { get; set; }
}
