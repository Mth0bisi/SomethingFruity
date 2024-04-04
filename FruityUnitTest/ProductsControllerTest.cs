using SomethingFruity.Data.Repository.Products;
using SomethingFruity.Models;
using SomethingFruity.Services;
using Moq;

namespace FruityUnitTest
{
    public class ProductsControllerTest
    {
        [Fact]
        public void GetAll_Product_Success()
        {
            //Arrange
            var mockProductsRepository = new Mock<IProductRepository>();
            var productsService = new ProductsService(mockProductsRepository.Object);
           //UserId can be retrieved from the database under the Users table after user has registered account
            var user = new User { Id = "UserId", Email = "user@gmai.com"};
            var productsList = new List<Product>
            {
                new Product { ProductId = 1, Name = "Apple",Description="", Price = 34, Code = "RED001",CategoryId = 1, UserId = user.Id, DateCreated = DateTime.Now},
                new Product { ProductId = 3, Name = "Avocado", Price = 45, Code = "RED001",CategoryId = 2, UserId = user.Id, DateCreated = DateTime.Now},
                new Product { ProductId = 4, Name = "Pear", Price = 28, Code = "RED001",CategoryId = 2, UserId = user.Id, DateCreated = DateTime.Now},
                new Product { ProductId = 2, Name = "Banana", Price = 32, Code = "RED001",CategoryId = 3, UserId = user.Id, DateCreated = DateTime.Now},
            };

            mockProductsRepository.Setup(repo => repo.GetAll()).Returns(productsList);

            //Act
            var result = productsService.GetUserProducts(user.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(productsList.Count, result.Count());
            Assert.Equal(productsList, result);
        }

        [Fact]
        public async void FindByName_Product_Success()
        {
            var mockProductsRepository = new Mock<IProductRepository>();
            var productsService = new ProductsService(mockProductsRepository.Object);
            var user = new User { Id = "UserId", Email = "user@gmai.com" };
            string producName = "Pear";
            var ExpectedProduct = new Product { ProductId = 4, Name = "Pear", Price = 28, Code = "RED001", CategoryId = 2, UserId = user.Id };

            mockProductsRepository.Setup(repo => repo.FindByNameAsync(producName, user.Id)).ReturnsAsync(ExpectedProduct);

            //Act
            var result = await productsService.FindByNameAsync(producName, user.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(ExpectedProduct.ProductId, result.ProductId);
            Assert.Equal(ExpectedProduct, result);
        }

        [Fact]
        public void Create_Product_Success()
        {
            //Arrange
            var mockProductsRepository = new Mock<IProductRepository>();
            var productsService = new ProductsService(mockProductsRepository.Object);
            var user = new User { Id = "UserId", Email = "user@gmai.com" };
            var newProduct = new Product { ProductId = 5, Name = "Lemon", Description = "pack", Price = 22, Code = "202409-111", CategoryId = 3, UserId = user.Id, DateCreated = DateTime.Now };

            //Act
            var result = productsService.Create(newProduct);

            //Assert
            mockProductsRepository.Verify(repo => repo.Create(newProduct), Times.Once);

        }

        [Fact]
        public void Delete_Product_Success()
        {
            //Arrange
            var mockProductsRepository = new Mock<IProductRepository>();
            var productsService = new ProductsService(mockProductsRepository.Object);
            var user = new User { Id = "UserId", Email = "user@gmai.com" };
            var dbProduct = new Product { ProductId = 4, Name = "Pear", Price = 28, Code = "RED001", CategoryId = 2, UserId = user.Id };
            //Act
            var result = productsService.Delete(dbProduct);

            //Assert
            mockProductsRepository.Verify(repo => repo.Delete(dbProduct), Times.Once);

        }
    }
}
