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

        public async Task<ProductListResponse> GetAllActiveProductsAsync(int pageNumber = 1, int pageSize = 20)
        {
            var query = _db.Products
                .Where(p => p.DeleteDate == null && p.InactiveDate == null);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = await query
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productDtos = products.Select(MapToDto).ToList();

            return new ProductListResponse
            {
                Products = productDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<ProductListResponse> GetDangerousDrugsAsync(int pageNumber = 1, int pageSize = 20)
        {
            var query = _db.Products
                .Where(p => p.Dangerous == true && p.DeleteDate == null && p.InactiveDate == null);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = await query
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productDtos = products.Select(MapToDto).ToList();

            return new ProductListResponse
            {
                Products = productDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
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

        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            return product != null ? MapToDto(product) : null;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.ProductId,
                Name = product.ProductCode ?? "No name",
                Description = product.ProductDescription ?? "No description",
                Category = "Veterinary", // Can be mapped from another table if needed
                Price = product.SupplierPrice ?? 0,
                IsActive = product.InactiveDate == null,
                IsDeleted = product.DeleteDate != null,
                IsDangerousDrug = product.Dangerous ?? false,
                CreatedAt = product.CreateDate,
                UpdatedAt = product.UpdateDate
            };
        }
    }
} 
