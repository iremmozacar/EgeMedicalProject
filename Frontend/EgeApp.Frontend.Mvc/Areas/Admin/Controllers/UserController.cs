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
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly INotyfService _notyfService;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notyfService = notyfService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

     
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notyfService.Error("Geçersiz kullanıcı ID'si.");
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("Kullanıcı bulunamadı.");
                return RedirectToAction("Index");
            }

            var model = new UpdateUserProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _roleManager.Roles.ToListAsync(),
                UserRoles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await _roleManager.Roles.ToListAsync();
                _notyfService.Error("Formda eksik veya hatalı bilgiler var.");
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                _notyfService.Error("Kullanıcı bulunamadı.");
                return RedirectToAction("Index");
            }

      
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    _notyfService.Error(error.Description);
                }
                model.Roles = await _roleManager.Roles.ToListAsync();
                return View(model);
            }

           
            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.UserRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(model.UserRoles).ToList();

            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                    return View(model);
                }
            }

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    foreach (var error in removeResult.Errors)
                    {
                        _notyfService.Error(error.Description);
                    }
                    return View(model);
                }
            }

            _notyfService.Success("Kullanıcı başarıyla güncellendi.");
            return RedirectToAction("Index");
        }
    


public async Task<IActionResult> Create()
        {
            var model = new CreateUserProfileViewModel
            {
                Roles = await _roleManager.Roles.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await _roleManager.Roles.ToListAsync();
                _notyfService.Error("Lütfen tüm gerekli alanları doldurun.");
                return View(model);
            }

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _notyfService.Error(error.Description);
                }
                model.Roles = await _roleManager.Roles.ToListAsync();
                return View(model);
            }

            if (model.UserRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, model.UserRoles);
            }

            _notyfService.Success("Kullanıcı başarıyla oluşturuldu.");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notyfService.Error("Geçersiz kullanıcı ID'si.");
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("Kullanıcı bulunamadı.");
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _notyfService.Error(error.Description);
                }

                return RedirectToAction("Edit", new { id });
            }

            _notyfService.Success("Kullanıcı başarıyla silindi.");
            return RedirectToAction("Index");
        }

    }
}