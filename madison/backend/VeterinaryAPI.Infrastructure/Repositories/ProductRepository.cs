using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Application.DTOs;
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

        public async Task<ProductListResponse> GetActiveProductsAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Products.AsNoTracking()
                .Where(p => p.DeleteDate == null && p.InactiveDate == null);

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.ProductCode ?? "Unknown",
                    Description = p.ProductDescription ?? "No description",
                    Category = p.ProductCode ?? "Unknown",
                    Price = p.SupplierPrice ?? 0,
                    IsActive = p.InactiveDate == null,
                    IsDeleted = p.DeleteDate != null,
                    IsDangerousDrug = p.Dangerous == true,
                    CreatedAt = p.CreateDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    UpdatedAt = p.UpdateDate != null ? p.UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null
                })
                .ToListAsync(cancellationToken);

            return new ProductListResponse
            {
                Products = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<ProductListResponse> GetDangerousDrugsAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default)
        {
            var query = _context.Products.AsNoTracking()
                .Where(p => p.Dangerous == true && p.DeleteDate == null && p.InactiveDate == null);

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .OrderByDescending(p => p.CreateDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.ProductCode ?? "Unknown",
                    Description = p.ProductDescription ?? "No description",
                    Category = p.ProductCode ?? "Unknown",
                    Price = p.SupplierPrice ?? 0,
                    IsActive = p.InactiveDate == null,
                    IsDeleted = p.DeleteDate != null,
                    IsDangerousDrug = p.Dangerous == true,
                    CreatedAt = p.CreateDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    UpdatedAt = p.UpdateDate != null ? p.UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null
                })
                .ToListAsync(cancellationToken);

            return new ProductListResponse
            {
                Products = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.AsNoTracking()
                .Where(p => p.ProductId == id && p.DeleteDate == null)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.ProductCode ?? "Unknown",
                    Description = p.ProductDescription ?? "No description",
                    Category = p.ProductCode ?? "Unknown",
                    Price = p.SupplierPrice ?? 0,
                    IsActive = p.InactiveDate == null,
                    IsDeleted = p.DeleteDate != null,
                    IsDangerousDrug = p.Dangerous == true,
                    CreatedAt = p.CreateDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    UpdatedAt = p.UpdateDate != null ? p.UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null
                })
                .FirstOrDefaultAsync(cancellationToken);

            return product;
        }

        public async Task<bool> UpdateDescriptionAsync(int id, string description, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.DeleteDate == null, cancellationToken);

            if (product == null)
                return false;

            product.UpdateDescription(description);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 
