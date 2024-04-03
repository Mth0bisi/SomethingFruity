using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using SomethingFruity.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using ANBU.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Serilog;
using ANBU.Data.Repository.Categories;
using ANBU.Services;
using ANBU.Models;

namespace SomethingFruity.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<User> _userManager;

        public ProductsController(IProductsService productService, ICategoryRepository categoryRepository, UserManager<User> userManager)
        {
            _productService = productService;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index(string returnUrl, string filter, string sortExpression = "Name", int pageIndex = 1)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userEmail);
            var products = _productService.GetUserProducts(user.Id);

            if (!String.IsNullOrEmpty(filter))
                products = products.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();

            List<ProductsVM> productsVMList = new List<ProductsVM>();

            foreach (var product in products)
            {
                var category = await _categoryRepository.GetById(product.CategoryId);
                if (category.IsActive == true)
                {
                    productsVMList.Add(new ProductsVM()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        CategoryName = category.Name,
                        CategoryId = category.CategoryId,
                        Code = product.Code,
                        DbImage = product.Image,
                        UserId = user.Id,
                        ImageName = product.ImageName,
                        DateCreated = product.DateCreated
                    });
                }

            }

            var model = PagingList.Create(productsVMList, 10, pageIndex, sortExpression, "Name");
            Log.Information($"Retrieved products for user {userEmail}.");
            ViewBag.returnUrl = returnUrl;

            return View(model);

        }

        public IActionResult Create()
        {
            var categories = _categoryRepository.GetAll();
            categories = categories.Where(i => i.IsActive == true).ToList();

            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsVM productVM)
        {
            var productExists = await _productService.FindByNameAsync(productVM.Name, productVM.UserId);

            if (productExists != null)
            {
                TempData["Error"] = "Product already exists.";
                Log.Information($"Can not create duplicate product. User: {User.Identity.Name}");
                return View(productVM);
            }
            else
            {
                try
                {
                    var userEmail = User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(userEmail);

                    if (productVM.Image != null && productVM.Image.Length > 0)
                    {
                        Product product = new Product()
                        {
                            Name = productVM.Name,
                            Description = productVM.Description,
                            Price = productVM.Price,
                            Image = ConvertIFormFileToByteArray(productVM.Image),
                            ImageName = productVM.Image.FileName,
                            UserId = user.Id,
                            CategoryId = productVM.CategoryId,
                            Code = _productService.GenerateValidatedProductCode(),
                            DateCreated = DateTime.Now
                        };

                        await _productService.Create(product);
                    }
                    else
                    {
                        Product product = new Product()
                        {
                            Name = productVM.Name,
                            Description = productVM.Description,
                            Price = productVM.Price,
                            UserId = user.Id,
                            CategoryId = productVM.CategoryId,
                            Code = _productService.GenerateValidatedProductCode(),
                            DateCreated = DateTime.Now
                        };

                        await _productService.Create(product);
                    }

                    TempData["Success"] = "Product successfully created!";
                    Log.Information($"Product {productVM.Name} created, by user: {User.Identity.Name}");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    Log.Information($"Error while creating product {productVM.Name} by User {User.Identity.Name}.");
                    return View();
                }
            }
        }

        public async Task<IActionResult> Edit(string name, string userId)
        {
            var product = await _productService.FindByNameAsync(name, userId);
            if (product == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetById(product.CategoryId);
            var productVM = new ProductsVM()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = category.Name,
                CategoryId = category.CategoryId,
                Code = product.Code,
                UserId= product.UserId,
                ImageName = product.ImageName,
                DbImage = product.Image
            };

            var categories = _categoryRepository.GetAll();
            categories = categories.Where(i => i.IsActive == true).ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductsVM productVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dbProduct = await _productService.FindByNameAsync(productVM.Name, productVM.UserId);
                    if (productVM.Image != null && productVM.Image.Length > 0)
                    {
                        dbProduct.Name = productVM.Name;
                        dbProduct.Description = productVM.Description;
                        dbProduct.Price = productVM.Price;
                        dbProduct.Image = ConvertIFormFileToByteArray(productVM.Image);
                        dbProduct.ImageName = productVM.Image.FileName;
                        dbProduct.UserId = productVM.UserId;
                        dbProduct.CategoryId = productVM.CategoryId;
                        dbProduct.Code = productVM.Code;

                        await _productService.Edit(dbProduct);
                    }
                    else
                    {
                        dbProduct.Name = productVM.Name;
                        dbProduct.Description = productVM.Description;
                        dbProduct.Price = productVM.Price;
                        dbProduct.UserId = productVM.UserId;
                        dbProduct.CategoryId = productVM.CategoryId;
                        dbProduct.Code = productVM.Code;

                        await _productService.Edit(dbProduct);
                    }

                    TempData["success"] = "Product successfully updated!";
                    Log.Information($"User {User.Identity.Name} updated product : {productVM.Name}.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    Log.Information($"Error while updating product {productVM.Name} by User {User.Identity.Name}.");
                    return NotFound();
                }
            
            }
            TempData["Error"] = "Failed to edit Product!";
            Log.Information($"Could not update product {productVM.Name} by User {User.Identity.Name}.");
            return View(productVM);
        }

        public IActionResult BulkCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkCreate(IFormFile upload)
        {
            var userEmail = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userEmail);

            if (upload != null && upload.Length > 0)
            {
                try
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "/Uploads/Files/" + upload.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await upload.CopyToAsync(stream);
                    }
                    Log.Information($"User {User.Identity.Name} initialized bulk products creation. File copied to local folder by system.");
                    await _productService.ExtractProducts(path, user.Id);

                    TempData["Success"] = "Bulk products successfully created!";
                    Log.Information($"Bulk products creation successfully created.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    Log.Information($"ERROR while bulk creating products, {ex.Message.ToString()}.");
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                TempData["error"] = "Error Uploading File";
                Log.Information($"System could not create the products from file.");
                return RedirectToAction(nameof(Create));
            }
        }

        public async Task<IActionResult> Delete(string name, string userId)
        {
            var product = await _productService.FindByNameAsync(name, userId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name, string userId)
        {
            try
            {
                var product = await _productService.FindByNameAsync(name, userId);
                await _productService.Delete(product);

                TempData["Success"] = "Product successfuly deleted!";
                Log.Information($"User {User.Identity.Name} has deleted the following product: {name}.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
                Log.Information($"ERROR while deleting product, {ex.Message.ToString()}.");
                return RedirectToAction(nameof(Create));
            }
        }

        public async Task<ActionResult> ExportToCSV(string filter)
        {
            try
            {
                var userEmail = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userEmail);
                var products = await _productService.GenerateProductsExport(filter, user.Id);
                var fileName = User.Identity.Name + "_Products Export_" + DateTime.Now.ToString("MM/dd/yyyy") + ".csv";

                if (!String.IsNullOrEmpty(filter))
                    fileName = User.Identity.Name + "_Products Export_%" + filter + "%_"+ DateTime.Now.ToString("MM/dd/yyyy") + ".csv";

                Log.Information($"User {User.Identity.Name} has exported products to csv file.");

                return File(Encoding.UTF8.GetBytes(products.ToString()), "text/csv", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating file.");

                TempData["error"] = "Bad Request!";
                return RedirectToAction("Index", "Products");
            }
        }

        private byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
