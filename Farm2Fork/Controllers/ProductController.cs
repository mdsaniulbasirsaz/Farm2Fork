using Microsoft.AspNetCore.Mvc;
using Farm2Fork.Models;
using Farm2Fork.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Repositories.Implementations;
using Farm2Fork.Helpers;
using Farm2Fork.Services.Implementations;
using Farm2Fork.Data;
using Microsoft.EntityFrameworkCore;

namespace Farm2Fork.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            try
            {
                await _productService.AddProductAsync(product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetProductsByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var products = await _productService.GetProductsByPriceRange(min, max);

            if (products == null || !products.Any())
            {
                return Ok("No products found in the specified price range.");
            }
            return Ok(products);
        }

        [HttpGet("search-by-name")]
        public async Task<ActionResult<IEnumerable<Product>>> FuzzySearchByName([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required");
            }

            var result = await _productService.FuzzySearchByName(searchTerm);
            if (result == null || !result.Any())
            {
                return NotFound("No products found matching the search term.");
            }

            return Ok(result);
        }

    }
}
