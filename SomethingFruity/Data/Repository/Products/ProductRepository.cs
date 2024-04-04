using ClosedXML.Excel;
using CsvHelper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using SomethingFruity.Models;
using System;
using System.Collections;
using System.Text;

namespace SomethingFruity.Data.Repository.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable> GetProductsByCategory(int id, string userId);
        IEnumerable<Product> GetUserProducts(string userId);
        Task ExtractProducts(string path, string userId);
        Task<StringBuilder> GenerateProductsExport(string path, string userId, string categoryFilter, int categoryId);
        Task<Product> FindByNameAsync(string name, string userId);
        public string GenerateValidatedProductCode();
    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SomethingFruityDbContext context) : base(context)
        {
        }

        public async Task<Product> FindByNameAsync(string name, string userId)
        {
            return await _entities.Where(p => p.Name == name && p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable> GetProductsByCategory(int id, string userId)
        {
            return await _context.Products.Where(p => p.CategoryId == id && p.UserId == userId).ToListAsync();
        }

        public IEnumerable<Product> GetUserProducts(string userId)
        {
            var products = GetAll();

            return products.Where(p => p.UserId.Equals(userId)).ToList();
        }

        public async Task ExtractProducts(string path, string userId)
        {
            List<Product> productsList = new List<Product>();

            using (var workbook = new XLWorkbook(path))
            {
                var worksheetCount = workbook.Worksheets.Count();
                if (worksheetCount > 0)
                {
                    var workSheet = workbook.Worksheet(1);
                    int totalRows = workSheet.RowsUsed().Count();
                    int totalColumns = workSheet.ColumnsUsed().Count();
                    try
                    {
                        for (int i = 2; i < totalRows; i++)
                        {
                            var categoryName = Convert.ToString(workSheet.Cell(i, 4).Value.ToString());
                            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
                            if (category == null)
                            {
                                return;
                            }
                            var productName = Convert.ToString(workSheet.Cell(i, 1).Value.ToString());
                            var dbProduct =  await _entities.Where(p => p.Name == productName && p.UserId == userId).FirstOrDefaultAsync();
                            if (dbProduct != null)
                            {
                                return;
                            }

                            productsList.Add(new Product
                            {
                                Name = workSheet.Cell(i, 1).Value.ToString(),
                                Description = workSheet.Cell(i, 2).Value.ToString().ToString(),
                                Price = Convert.ToDouble(workSheet.Cell(i, 3).Value.ToString()),
                                Code = GenerateValidatedProductCode(),
                                CategoryId = category.CategoryId,
                                UserId = userId,
                                DateCreated = DateTime.Now,
                            });
                        }

                        await CreateRange(productsList);
                        await Save();
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

        }

        public async Task<StringBuilder> GenerateProductsExport(string filter, string userId, string categoryFilter, int categoryId)
        {
            try
            {
                var userProducts = await _context.Products.Where(p => p.UserId.Equals(userId)).ToListAsync();
                if (!string.IsNullOrEmpty(filter))
                    userProducts = userProducts.Where(p => p.Name.Contains(filter)).ToList();

                if (!String.IsNullOrEmpty(categoryFilter))
                    userProducts = userProducts.Where(p => p.CategoryId == categoryId).ToList();

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Name,Description,Price,Code,Category");

                foreach (var userProduct in userProducts)
                {
                    var category = _context.Categories.FirstOrDefault(c => c.CategoryId == userProduct.CategoryId);
                    stringBuilder.AppendLine($"{userProduct.Name},{userProduct.Description},{userProduct.Price},{userProduct.Code},{category.Name}");
                }

                return stringBuilder;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating file.");

            }

            return new StringBuilder();

        }

        public string GenerateValidatedProductCode()
        {
            bool validated = false;
            var validatedCode = "";
            while (!validated)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 1000);
                string formattedNumber = randomNumber.ToString("000");
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                validatedCode = year + month.ToString("00") + "-" + formattedNumber;

                var codeExists = _context.Products.Where(c => c.Code.Equals(validatedCode)).FirstOrDefault();

                if (codeExists == null)
                    validated = true;
            }

            return validatedCode;
        }
    }
}
