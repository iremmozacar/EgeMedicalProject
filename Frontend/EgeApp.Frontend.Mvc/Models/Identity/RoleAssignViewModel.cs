using System;
using EgeApp.Frontend.Mvc.Data.Entities;

namespace EgeApp.Frontend.Mvc.Models.Identity;

public class RoleAssignViewModel
{
    public string RoleId { get; set; }
    public AppRole Role { get; set; }
    public List<AppUser> Members { get; set; } = new();
    public List<AppUser> NonMembers { get; set; } = new();
    public List<string> IdsAdd { get; set; } = new();
    public List<string> IdsRemove { get; set; } = new();
}