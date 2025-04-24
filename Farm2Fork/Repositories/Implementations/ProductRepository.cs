// File: Farm2Fork/Repositories/Implementations/ProductRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Farm2Fork.Models;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Data;
using System.Security.Principal;

namespace Farm2Fork.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            // return await _context.Products
            // .Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();

            var products = await _context.Products
                .OrderBy(p => p.Price)
                .ToListAsync();

            var minIndex = BinarySearch(products, minPrice, true);
            var maxIndex = BinarySearch(products, maxPrice, false);

            if (minIndex == -1 || maxIndex == -1)
            {

                return Enumerable.Empty<Product>();
            }
            return products.Skip(minIndex).Take(maxIndex - minIndex + 1);
        }

        private static int BinarySearch(List<Product> products, decimal targetPrice, bool isLowerBound)
        {
            int left = 0;
            int right = products.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                var price = products[mid].Price;

                if (price == targetPrice)
                {
                    return mid;
                }
                else if (price < targetPrice)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return isLowerBound ? left : right;
        }

        //Fuzzy Search by name using Levenshtein Distance
        public async Task<IEnumerable<Product>> FuzzySearchByName(string searchTerm)
        {
            //Fetch All Products from DB
            var products = await _context.Products.ToListAsync();
            var result = products.Where(product => LevenshteinDistance(product.Name.ToLower(), searchTerm.ToLower()) <= 3).ToList();
            return result;
        }

        //Levenshtein Distance implementation
        public static int LevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                return target?.Length ?? 0;
            }

            if (string.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            var n = source.Length;
            var m = target.Length;
            var d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
    }
}
