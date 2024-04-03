﻿using ANBU.Data.Repository.Categories;
using ANBU.Models;

namespace ANBU.Services
{
    public interface ICategoriesService 
    {
        Task<Category> FindByNameAsync(string name);
        List<Category> GetCategories();
        Task Create(Category category);
        Task<Category> Edit(Category category);
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Create(Category category)
        {
            await _categoryRepository.Create(category);
            await _categoryRepository.Save();
        }

        public async Task<Category> Edit(Category category)
        {
            _categoryRepository.Update(category);
            await _categoryRepository.Save();

            return category;
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _categoryRepository.FindByNameAsync(name);
        }

        public List<Category> GetCategories()
        {
            return  _categoryRepository.GetAll();
        }
    }
}
