using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Models.Category;
using EgeApp.Frontend.Mvc.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EgeApp.Frontend.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin, Admin")]
    public class CategoryController : Controller
    {
        private readonly INotyfService _notyfService;

        public CategoryController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await CategoryService.GetAllAsync();
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.Error;
                return RedirectToAction("Error", "Home");
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryCreateViewModel { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Lütfen tüm bilgileri doğru şekilde doldurun.");
                return View(model);
            }

            if (model.Image != null)
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Image.FileName)}";
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/categories", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    model.ImageUrl = $"/uploads/categories/{fileName}";
                }
                catch (Exception ex)
                {
                    _notyfService.Error($"Görsel yüklenirken hata oluştu: {ex.Message}");
                    return View(model);
                }
            }

            var result = await CategoryService.CreateAsync(model);
            if (!result.IsSucceeded)
            {
                _notyfService.Error(result.Error ?? "Kategori oluşturulamadı.");
                return View(model);
            }

            _notyfService.Success("Kategori başarıyla oluşturuldu.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                _notyfService.Error("Geçersiz kategori ID'si.");
                return RedirectToAction("Index");
            }

            var response = await CategoryService.GetByIdAsync(id);
            if (!response.IsSucceeded || response.Data == null)
            {
                _notyfService.Error(response.Error ?? "Kategori bulunamadı.");
                return RedirectToAction("Index");
            }

            var model = new CategoryUpdateViewModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                Description = response.Data.Description,
                IsActive = response.Data.IsActive,
                IsHome = response.Data.IsHome,
                ImageUrl = response.Data.ImageUrl
            };

           
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Lütfen tüm bilgileri doğru şekilde doldurun.");
                return View(model);
            }

            if (model.Image != null)
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Image.FileName)}";
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/categories", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    model.ImageUrl = $"/uploads/categories/{fileName}";
                }
                catch (Exception ex)
                {
                    _notyfService.Error($"Görsel yüklenirken hata oluştu: {ex.Message}");
                    return View(model);
                }
            }
            
            var result = await CategoryService.UpdateAsync(model);
            if (!result.IsSucceeded)
            {
                _notyfService.Error(result.Error ?? "Kategori güncellenemedi.");
                return View(model);
            }

            _notyfService.Success("Kategori başarıyla güncellendi");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                _notyfService.Error("Geçersiz kategori ID'si.");
                return RedirectToAction("Index");
            }

            var response = await CategoryService.GetByIdAsync(id);
            if (!response.IsSucceeded || response.Data == null)
            {
                _notyfService.Error(response.Error ?? "Kategori bulunamadı.");
                return RedirectToAction("Index");
            }

            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (id <= 0)
            {
                _notyfService.Error("Geçersiz kategori ID'si.");
                return RedirectToAction("Index");
            }

            var response = await CategoryService.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                _notyfService.Error(response.Error ?? "Kategori silinemedi.");
                return RedirectToAction("Index");
            }

            _notyfService.Success("Kategori başarıyla silindi.");
            return RedirectToAction("Index");
        }
    }
}