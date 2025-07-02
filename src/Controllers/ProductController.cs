using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Product;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                var result = new ApiResult<IEnumerable<ProductDto>>(products, true, "Products fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                var result = new ApiResult<ProductDto>(product, true, "Product fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
        {
            try
            {
                var newProduct = await _productService.CreateProductAsync(product);
                var result = new ApiResult<ProductDto>(newProduct, true, "Product created successfully");
                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, product);
                var result = new ApiResult<ProductDto>(updatedProduct, true, "Product updated successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var deletedProduct = await _productService.DeleteProductAsync(id);
                var result = new ApiResult<ProductDto>(deletedProduct, true, "Product deleted successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}