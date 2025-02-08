using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductsAPI.Controllers;
using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Models.Domain;
using ProductsAPI.Models.DTO;

namespace TestProductEndpoints
{
    public class Tests
    {
        private ProductsController _controller;
        private Mock<ApplicationDBContext> _mockContext;
        private Mock<DbSet<Products>> _mockSet;

        [SetUp]
        public void Setup()
        {
            var products = new List<Products>
        {
            new Products { Id = 1, Name = "Chairs", Price = 10, StockAvailable = 100 }
        }.AsQueryable();

            _mockSet = new Mock<DbSet<Products>>();
            _mockSet.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(products.Provider);
            _mockSet.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(products.Expression);
            _mockSet.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(products.ElementType);
            _mockSet.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            _mockContext = new Mock<ApplicationDBContext>(new DbContextOptions<ApplicationDBContext>());

            _mockContext.Setup(x => x.Products)
                .Returns(_mockSet.Object);

            var _helper = new Helper(_mockContext.Object);   
            _controller = new ProductsController(_mockContext.Object, _helper);

        }

        [Test]
 
        public async Task AddProduct_ShouldCreatedAtAction()
        {
            var product = new CreateProductDto { Name = "Moq Product", Price = 25, StockAvailable = 150 };
            var result = await _controller.AddProduct(product);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }


        [Test]
        public async Task AddToStock_ShouldIncraseStockCount()
        {
            var productId = _mockContext.Object.Products.First().Id;
            var product = _mockContext.Object.Products.First();

            _mockSet.Setup(m => m.FindAsync(productId)).ReturnsAsync(product);

            var result = await _controller.AddToStock(productId, 20);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var updatedProduct = okResult.Value as ProductDto;
            Assert.That(updatedProduct.StockAvailable, Is.EqualTo(120));
        }
    }
}