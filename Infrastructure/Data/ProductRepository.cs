using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var productBrands = await _context.ProductBrands.ToListAsync();
            return productBrands;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(prod => prod.Id == id);
            return product; 
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var ProductTypes = await _context.ProductTypes.ToListAsync();
            return ProductTypes;
        }
    }
}