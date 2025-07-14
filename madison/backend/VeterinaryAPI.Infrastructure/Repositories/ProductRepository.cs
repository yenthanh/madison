using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Application.Interfaces;
using VeterinaryAPI.Domain.Entities;
using VeterinaryAPI.Infrastructure.Data;

namespace VeterinaryAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllActiveAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Where(p => p.DeleteDate == null && p.InactiveDate == null)
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDangerousDrugsAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Where(p => p.Dangerous == true && p.DeleteDate == null && p.InactiveDate == null)
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await _context.Products
                .Where(p => p.DeleteDate == null && p.InactiveDate == null)
                .CountAsync();
        }

        public async Task<int> GetDangerousDrugsCountAsync()
        {
            return await _context.Products
                .Where(p => p.Dangerous == true && p.DeleteDate == null && p.InactiveDate == null)
                .CountAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                product.SoftDelete();
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == id);
        }
    }
} 
