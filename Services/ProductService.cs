using VeterinaryAPI.Models;
using VeterinaryAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VeterinaryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly MadisonDbContext _db;

        public ProductService(MadisonDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllActiveProductsAsync()
        {
            return await _db.Products
                .Where(p => p.DeleteDate == null && p.InactiveDate == null)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDangerousDrugsAsync()
        {
            return await _db.Products
                .Where(p => p.Dangerous == true && p.DeleteDate == null && p.InactiveDate == null)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
                return false;

            product.ProductDescription = description;
            product.UpdateDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }
    }
} 
