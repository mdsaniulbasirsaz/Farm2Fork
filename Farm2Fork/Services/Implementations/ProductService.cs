using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.Models;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Helpers;
using Farm2Fork.Services.Interfaces;

namespace Farm2Fork.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // No need for ImageHelper as it is static
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                // Directly call the static method of ImageHelper
                string base64Image = ImageHelper.ConvertImageToBase64(product.ImageUrl);
                product.ImageUrl = base64Image;
            }

            await _productRepository.AddProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _productRepository.GetProductsByPriceRange(minPrice, maxPrice);
        }

        public async Task<IEnumerable<Product>> FuzzySearchByName(string searchTerm)
        {
            return await _productRepository.FuzzySearchByName(searchTerm);
        }

    }
}
