using GenericOps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericsController(IRepository<Product> _productRepository) : ControllerBase
    {
        [HttpGet("GetProductsAsync")]
        public async Task<ActionResult> GetProductsAsync()
        {
            IEnumerable<Product> products;
            Func<Product, bool> filter = p => p.IsActive > 0;
            products = await _productRepository.GetAll(filter);
            return Ok(products);
        }

        [HttpPost("PostProductAsync")]
        public async Task<ActionResult> PostProductAsync(Product entity)
        {
            int isCreated = await _productRepository.PostEntity(entity);
            string message = (isCreated > 0) ? "Product created successfully" : "Failed to create product";
            return Ok(message);
        }

        [HttpGet("GetProductDetails")]
        public async Task<ActionResult> GetSingleProduct(int productId)
        {
            Expression<Func<Product, bool>>? predicate = p => p.ProductId == productId;

            var productDetails = (await _productRepository.GetByIdAsync(predicate,
                                p => new
                                {
                                    p.ProductId,
                                    p.ProductName,
                                    p.ProductPrice,
                                    p.AddedOn,
                                    CategoryName = p.Category.CategoryName
                                }));
            return Ok(productDetails);
        }

        [HttpPut("UpdateProductAsync")]
        public async Task<ActionResult> UpdateProductAsync(Product product)
        {
            Func<Product, bool>? predicate = p => p.ProductId == product.ProductId;
            Product? findProduct = await _productRepository.GetByIdAsync(
                                  p => p.ProductId == product.ProductId,
                                  p => p
                                  );
            Action<Product> updateAction = (p) =>
            {
                p.ProductName = product.ProductName;
                p.ProductPrice = product.ProductPrice;
            };
            int updateResult = await _productRepository.UpdateEntity(predicate, updateAction);
            string message = updateResult > 0 ? "Product Updated successfully" : "Failed to update product";
            return Ok(message);
        }
    }
}
