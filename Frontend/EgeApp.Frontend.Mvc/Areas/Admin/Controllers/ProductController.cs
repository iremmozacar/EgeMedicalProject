using System;
using System.IO;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Models.Product;
using EgeApp.Frontend.Mvc.Services;

namespace EgeApp.Frontend.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin, Admin")]
    public class ProductController : Controller
    {
        private readonly INotyfService _notyfService;

        public ProductController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await ProductService.GetAllAsync();
            if (!result.IsSucceeded)
            {
                TempData["Error"] = result.Error ?? "Ürünler yüklenirken bir hata oluştu.";
                return RedirectToAction("Error", "Home");
            }
            return View(result.Data);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateViewModel();

            var categoriesResponse = await CategoryService.GetSelectListItemsAsync();
            if (categoriesResponse.IsSucceeded)
            {
                model.CategoryList = categoriesResponse.Data;
            }
            else
            {
                TempData["Error"] = categoriesResponse.Error ?? "Kategoriler yüklenemedi.";
            }

            return View(model);
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Super Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Lütfen tüm alanları doldurun.");
                var categoriesResponse = await CategoryService.GetSelectListItemsAsync();
                if (categoriesResponse.IsSucceeded)
                {
                    model.CategoryList = categoriesResponse.Data;
                }
                return View(model);
            }

            if (model.Image != null)
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Image.FileName)}";
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    model.ImageUrl = $"/uploads/products/{fileName}";
                }
                catch (Exception ex)
                {
                    _notyfService.Error($"Görsel yüklenirken hata oluştu: {ex.Message}");
                    return View(model);
                }
            }

            var result = await ProductService.CreateAsync(model);
            if (!result.IsSucceeded)
            {
                _notyfService.Error(result.Error ?? "Ürün oluşturulamadı.");
                return View(model);
            }

            _notyfService.Success("Ürün başarıyla oluşturuldu.");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await ProductService.GetByIdAsync(id);
            if (!result.IsSucceeded)
            {
                TempData["Error"] = result.Error ?? "Ürün bulunamadı.";
                return RedirectToAction("Error", "Home");
            }

            var model = new ProductUpdateViewModel
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Price = result.Data.Price,
                DiscountedPrice = result.Data.DiscountedPrice,
                ImageUrl = result.Data.ImageUrl,
                IsActive = result.Data.IsActive,
                IsDiscounted = result.Data.IsDiscounted,
                IsFreeShipping = result.Data.IsFreeShipping,
                IsSpecialProduct = result.Data.IsSpecialProduct,
                IsSameDayShipping = result.Data.IsSameDayShipping,
                CategoryId = result.Data.CategoryId,
                Url = result.Data.Url,
                Brand = result.Data.Brand,
                IsHome = result.Data.IsHome,
                Description = result.Data.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Lütfen tüm gerekli alanları doldurun.");
                return View(model);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadResult = await UploadImageAsync(imageFile);
                if (!uploadResult.IsSuccess)
                {
                    TempData["Error"] = "Fotoğraf yüklenirken bir hata oluştu.";
                    return View(model);
                }

                model.ImageUrl = uploadResult.ImageUrl; // Fotoğraf URL'si API'ye gönderilecek.
            }

            var result = await ProductService.UpdateAsync(model);
            if (!result.IsSucceeded)
            {
                TempData["Error"] = result.Error ?? "Ürün güncellenemedi.";
                return View(model);
            }

            _notyfService.Success("Ürün başarıyla güncellendi.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await ProductService.DeleteAsync(id);
            if (!result.IsSucceeded)
            {
                TempData["Error"] = result.Error ?? "Ürün silinemedi.";
                return RedirectToAction("Error", "Home");
            }

            _notyfService.Success("Ürün başarıyla silindi.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIsHome(int id)
        {
            var result = await ProductService.UpdateIsHomeAsync(id);
            if (!result.IsSucceeded)
            {
                ViewData["Error"] = result.Error ?? "Ürün güncellenemedi.";
                return Redirect("/home/error");
            }

            return Json(new { isHome = result.Data.IsHome });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIsActive(int id)
        {
            var result = await ProductService.UpdateIsActiveAsync(id);
            if (!result.IsSucceeded)
            {
                ViewData["Error"] = result.Error ?? "Ürün güncellenemedi.";
                return Redirect("/home/error");
            }

            return Json(new { isActive = result.Data.IsActive });
        }

        private async Task<(bool IsSuccess, string ImageUrl)> UploadImageAsync(IFormFile imageFile)
        {
            try
            {
              
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder); 
                }

               
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

               
                var imageUrl = $"/images/{uniqueFileName}";
                return (true, imageUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fotoğraf yükleme hatası: {ex.Message}");
                return (false, null); 
            }
        }
    }
}
