﻿using ANBU.Data.Repository.Categories;
using ANBU.Data.Repository.Products;
using ANBU.Models;
using System.Collections;
using System.Text;

namespace ANBU.Services
{
    public interface IProductsService
    {
        Task<IEnumerable> GetProductsByCategory(int id, string userId);
        IEnumerable<Product> GetUserProducts(string userId);
        Task ExtractProducts(string path, string userId);
        Task<StringBuilder> GenerateProductsExport(string path, string userId);
        Task<Product> FindByNameAsync(string name, string userId);
        public string GenerateValidatedProductCode();
        Task Create(Product product);
        Task<Product> Edit(Product product);
        Task Delete(Product product);
    }
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productsRepository;
        public ProductsService(IProductRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task Create(Product product)
        {
            await _productsRepository.Create(product);
            await _productsRepository.Save();
        }

        public async Task Delete(Product product)
        {
            _productsRepository.Delete(product);
            await _productsRepository.Save();
        }

        public async Task<Product> Edit(Product product)
        {
            _productsRepository.Update(product);
            await _productsRepository.Save();

            return product;
        }

        public async Task ExtractProducts(string path, string userId)
        {
             await _productsRepository.ExtractProducts(path, userId);
        }

        public async Task<Product> FindByNameAsync(string name, string userId)
        {
            return await _productsRepository.FindByNameAsync(name, userId);
        }

        public async Task<StringBuilder> GenerateProductsExport(string path, string userId)
        {
            return await _productsRepository.GenerateProductsExport(path, userId);
        }

        public string GenerateValidatedProductCode()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetProductsByCategory(int id, string userId)
        {
            return await _productsRepository.GetProductsByCategory(id, userId);
        }

        public IEnumerable<Product> GetUserProducts(string userId)
        {
            return _productsRepository.GetUserProducts(userId);
        }
    }
}
