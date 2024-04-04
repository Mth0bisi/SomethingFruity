using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SomethingFruity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SomethingFruity.Data.Repository.Categories;
using SomethingFruity.Services;
using SomethingFruity.Models.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using ReflectionIT.Mvc.Paging;
using Serilog;

namespace SomethingFruity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ICategoriesService _categoryService;
        private readonly UserManager<User> _userManager;

        public HomeController(IProductsService productService, ICategoriesService categoryService, UserManager<User> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index(string sortExpression = "CategoryName", int pageIndex = 1)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userEmail.ToLower());
            var products = _productService.GetUserProducts(user.Id);
            var categories = _categoryService.GetCategories();

            List<DashboardVM>  dashboardVM = new List<DashboardVM>();  

            foreach (var category in categories) 
            {
                var productCount = products.Where(c=>c.CategoryId == category.CategoryId).Count();
                dashboardVM.Add(new DashboardVM
                {
                    CategoryName = category.Name,
                    ProductsCount = productCount,
                    CategoryId = category.CategoryId
                });
            }
            var model = PagingList.Create(dashboardVM, 10, pageIndex, sortExpression, "Name");
            Log.Information($"Dashboard for user {userEmail}.");

            return View(model);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AccessDenied()
        {
            return View();
        }

        protected string Greetings()
        {
            string message;
            if (DateTime.Now.Hour < 12)
            {
                message = "Good Morning";
            }
            else if (DateTime.Now.Hour < 17)
            {
                message = "Good Afternoon";
            }
            else
            {
                message = "Good Evening";
            }
            return message;
        }
    }
}
