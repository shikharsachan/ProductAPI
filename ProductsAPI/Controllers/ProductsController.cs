using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;
using ProductsAPI.Models.Domain;
using ProductsAPI.Models.DTO;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IHelper _helper;
        public ProductsController(ApplicationDBContext dbContext, IHelper helper)
        {
            _dbContext = dbContext;
            _helper = helper;
        }

        #region HttpGet
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _dbContext.Products.ToListAsync();

            List<ProductDto> productDto = new List<ProductDto>();

            //Map Domain to DTO
            foreach (var product in products)
            {
                productDto.Add(new ProductDto()
                {
                    Id = product.Id,
                    ProductNumber = product.ProductNumber,
                    Name = product.Name,
                    Price = product.Price,
                    StockAvailable= product.StockAvailable
                });
            }
            if (products == null)
            {
                return NoContent();
            }
            else
                return Ok(productDto);
        }
        #endregion

        #region HttpGet By ID
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {

            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("No Product Available By The Given Id.");
            }
            else
            {
                //Map Domain to DTO
                ProductDto productDto = new ProductDto()
                {
                    Id = product.Id,
                    ProductNumber = product.ProductNumber,
                    Name = product.Name,
                    Price = product.Price,
                    StockAvailable = product.StockAvailable
                };

                return Ok(productDto);
            }
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto addToInventry)
        {
            Products item = new Products()
            {
                ProductNumber = _helper.GenerateUniqueId(),
                Name = addToInventry.Name,
                Price = addToInventry.Price,
                StockAvailable = addToInventry.StockAvailable,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Products.Add(item);
            await _dbContext.SaveChangesAsync();

            //Map Domain to DTO
            CreateProductDto productDto = new CreateProductDto()
            {
                Name = item.Name,
                Price = item.Price,
                StockAvailable = item.StockAvailable
            };

            return CreatedAtAction(nameof(AddProduct), new {id = item.Id}, productDto);
        }
        #endregion

        #region HttpDelete
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            if(id ==0)
            {
                return BadRequest();
            }

            var removeFromInventry = await _dbContext.Products.FindAsync(id);
            if(removeFromInventry == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(removeFromInventry);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region HttpPut
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]UpdateProductDto updateProduct)
        {

            var updateModel = await _dbContext.Products.FindAsync(id);
            if(updateModel == null)
            {
                return NotFound();
            }

            updateModel.Name = updateProduct.Name;
            updateModel.Price = updateProduct.Price; 
            updateModel.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            //Map Domain to DTO
            var productDto = new ProductDto()
            {
                Id = updateModel.Id,
                ProductNumber = updateModel.ProductNumber,
                Name = updateModel.Name,
                Price = updateModel.Price,
                StockAvailable = updateModel.StockAvailable
            };
            return Ok(productDto);

        }
        #endregion

        #region HttpPut DecrementStock
        [HttpPut("decrement-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {

            var updateProductStock = await _dbContext.Products.FindAsync(id);
            if (updateProductStock == null)
            {
                return NotFound();
            }

            if(updateProductStock.StockAvailable < quantity)
            {
                return BadRequest("Not Sufficient Stock");
            }

            updateProductStock.StockAvailable -= quantity;
            updateProductStock.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            //Map Domain to DTO
            var productDto = new ProductDto()
            {
                Id = updateProductStock.Id,
                ProductNumber = updateProductStock.ProductNumber,
                Name = updateProductStock.Name,
                Price = updateProductStock.Price,
                StockAvailable = updateProductStock.StockAvailable
            };
            return Ok(productDto);

        }
        #endregion

        #region HttpPut AddStock
        [HttpPut("add-to-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {

            var updateProductStock = await _dbContext.Products.FindAsync(id);
            if (updateProductStock == null)
            {
                return NotFound();
            }
            updateProductStock.StockAvailable += quantity;
            updateProductStock.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            //Map Domain to DTO
            var productDto = new ProductDto()
            {
                Id = updateProductStock.Id,
                ProductNumber = updateProductStock.ProductNumber,
                Name = updateProductStock.Name,
                Price = updateProductStock.Price,
                StockAvailable = updateProductStock.StockAvailable
            };
            return Ok(productDto);
        }
        #endregion
    }
}
