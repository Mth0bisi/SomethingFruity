using ANBU.Data.Repository.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Serilog;
using ANBU.Models;
using ANBU.Services;

namespace ANBU.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoryService;
        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<ActionResult> Index(string returnUrl, string filter, string sortExpression = "Name", int pageIndex = 1)
        {
            var categories = _categoryService.GetCategories();
            categories = categories.Where(i=>i.IsActive == true).ToList();

            if (!String.IsNullOrEmpty(filter))
                categories = categories.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();

            var model = PagingList.Create(categories, 10, pageIndex, sortExpression, "Name");
            ViewBag.returnUrl = returnUrl;
            Log.Information($"Retrieved all acive categories.");

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var categoryExists = await _categoryService.FindByNameAsync(category.Name);

            if (categoryExists != null)
            {
                TempData["Error"] = "Category already exists."; 
                Log.Information($"Can not create duplicate category. User: {User.Identity.Name}");
                return View(category);
            }
            else 
            {
                try
                {
                    category.DateCreated = DateTime.Now;
                    category.CreatedBy = User.Identity.Name;
                    await _categoryService.Create(category);

                    TempData["Success"] = "Category successfully created!";
                    Log.Information($"Category {category.Name} created, by user: {User.Identity.Name}");
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    TempData["Error"] = ex.Message;
                    return View();
                }
            }
        }

        public async Task<IActionResult> Edit(string name, string returnUrl)
        {
            var category = await _categoryService.FindByNameAsync(name);
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = returnUrl;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Edit(category);

                    TempData["success"] = "Category successfully updated!";
                    Log.Information($"User {User.Identity.Name} updated category : {category.Name}.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    Log.Information($"Error while updating category {category.Name} by User {User.Identity.Name}.");
                    return NotFound();
                }

            }
            TempData["Error"] = "Failed to edit Category!";
            Log.Information($"Could not update category {category.Name} by User {User.Identity.Name}.");
            return View(category);
        }

    }
}
