using SomethingFruity.Controllers;
using SomethingFruity.Data.Repository;
using SomethingFruity.Data.Repository.Categories;
using SomethingFruity.Models;
using SomethingFruity.Services;
using Moq;
using NuGet.Packaging.Signing;

namespace FruityUnitTest
{
    public class CategoriesControllerTest
    {
        [Fact]
        public void GetAll_Category_Success()
        {
            //Arrange
            var mockCategoriesRepository = new Mock<ICategoryRepository>();
            var categoriesService = new CategoriesService(mockCategoriesRepository.Object);
            var categoryList = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Red", CategoryCode = "RED001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now},
                new Category { CategoryId = 2, Name = "Green", CategoryCode = "GRE001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now},
                new Category { CategoryId = 3, Name = "Yellow", CategoryCode = "YEL001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now}
            };

            mockCategoriesRepository.Setup(repo => repo.GetAll()).Returns(categoryList);

            //Act
            var result = categoriesService.GetCategories();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(categoryList.Count, result.Count);
            Assert.Equal(categoryList, result);
        }

        [Fact]
        public async void FindByName_Category_Success()
        {
            //Arrange
            var mockCategoriesRepository = new Mock<ICategoryRepository>();
            var categoriesService = new CategoriesService(mockCategoriesRepository.Object);
            string catgoryName = "Green";
            var ExpectedCategory = new Category { CategoryId = 2, Name = "Green", CategoryCode = "GRE001", CreatedBy = "user@gmail.com" };

            mockCategoriesRepository.Setup(repo => repo.FindByNameAsync(catgoryName)).ReturnsAsync(ExpectedCategory);

            //Act
            var result = await categoriesService.FindByNameAsync(catgoryName);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(ExpectedCategory.CategoryId, result.CategoryId);
            Assert.Equal(ExpectedCategory, result);
        }
        
        [Fact]
        public void Create_Category_Success()
        {
            //Arrange
            var mockCategoriesRepository = new Mock<ICategoryRepository>();
            var categoriesService = new CategoriesService(mockCategoriesRepository.Object);
            var newCategory = new Category { CategoryId = 4, Name = "Orange", CategoryCode = "ORA001", CreatedBy = "user@gmail.com" };

            //Act
            var result = categoriesService.Create(newCategory);

            //Assert
            mockCategoriesRepository.Verify(repo => repo.Create(newCategory), Times.Once);

        }
        
        [Fact]
        public void Update_Category_Success()
        {
            //Arrange
            var mockCategoriesRepository = new Mock<ICategoryRepository>();
            var categoriesService = new CategoriesService(mockCategoriesRepository.Object);
            var dbCategory = new Category { CategoryId = 2, Name = "Green", CategoryCode = "GRE001", CreatedBy = "user@gmail.com" };

            //Act
            var result = categoriesService.Edit(dbCategory);

            //Assert
            mockCategoriesRepository.Verify(repo => repo.Update(dbCategory), Times.Once);

        }
    }
}