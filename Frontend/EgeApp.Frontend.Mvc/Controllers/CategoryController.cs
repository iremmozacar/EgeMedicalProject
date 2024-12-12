using Microsoft.AspNetCore.Mvc;
using EgeApp.Frontend.Mvc.Models.Category;
using EgeApp.Frontend.Mvc.Services;

namespace EgeApp.Frontend.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await CategoryService.GetActives();
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.Error;
                return RedirectToAction("Error", "Home");
            }
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View(new CategoryCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await CategoryService.CreateAsync(model);
            if (!response.IsSucceeded)
            {
                ModelState.AddModelError("", response.Error);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await CategoryService.GetByIdAsync(id);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.Error;
                return RedirectToAction("Error", "Home");
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await CategoryService.UpdateAsync(model);
            if (!response.IsSucceeded)
            {
                ModelState.AddModelError("", response.Error);
                return View(model);
            }
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var response = await CategoryService.GetByIdAsync(id);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.Error;
                return RedirectToAction("Error", "Home");
            }
            return View(response.Data); 
        }

   
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var response = await CategoryService.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.Error;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index");
        }

    }
}
