using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.Models;
namespace Farm2Fork.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        // Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        // Task UpdateProductAsync(Product product);
        // Task DeleteProductAsync(int id);

        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        
    }
}