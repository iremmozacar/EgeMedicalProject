using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EgeApp.Frontend.Mvc.Data.Entities;
using EgeApp.Frontend.Mvc.Models.Identity;

namespace EgeApp.Frontend.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly INotyfService _notyfService;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notyfService = notyfService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notyfService.Error("Rol ID'si eksik veya geçersiz.");
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _notyfService.Error("Rol bulunamadı.");
                return RedirectToAction("Index");
            }

            var users = await _userManager.Users.ToListAsync();
            var members = new List<AppUser>();
            var nonMembers = new List<AppUser>();

            foreach (var user in users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }

            var model = new RoleAssignViewModel
            {
                RoleId = role.Id,
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleAssignViewModel model)
        {
            if (string.IsNullOrEmpty(model.RoleId))
            {
                _notyfService.Error("Rol ID'si eksik veya gönderilmedi.");
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                _notyfService.Error("Rol bulunamadı.");
                return RedirectToAction("Index");
            }

            foreach (var userId in model.IdsAdd ?? new List<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _notyfService.Error($"Kullanıcı bulunamadı: {userId}");
                    continue;
                }

                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                }
            }

            foreach (var userId in model.IdsRemove ?? new List<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _notyfService.Error($"Kullanıcı bulunamadı: {userId}");
                    continue;
                }

                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                }
            }

            _notyfService.Success("Rol/Kullanıcı atama işlemleri başarıyla tamamlandı.");
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddUsersToRole(string roleId, List<string> userIdsToAdd)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                _notyfService.Error("Rol ID'si eksik.");
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                _notyfService.Error("Rol bulunamadı.");
                return RedirectToAction("Index");
            }

            foreach (var userId in userIdsToAdd ?? new List<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _notyfService.Error($"Kullanıcı bulunamadı: {userId}");
                    continue;
                }

                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                }
            }

            _notyfService.Success("Kullanıcılar başarıyla role eklendi.");
            return RedirectToAction("Edit", new { id = roleId });
        }

     
        [HttpPost]
        public async Task<IActionResult> RemoveUsersFromRole(string roleId, List<string> userIdsToRemove)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                _notyfService.Error("Rol ID'si eksik.");
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                _notyfService.Error("Rol bulunamadı.");
                return RedirectToAction("Index");
            }

            foreach (var userId in userIdsToRemove ?? new List<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _notyfService.Error($"Kullanıcı bulunamadı: {userId}");
                    continue;
                }

                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                }
            }

            _notyfService.Success("Kullanıcılar başarıyla rolden çıkarıldı.");
            return RedirectToAction("Edit", new { id = roleId });
        }
    }
}