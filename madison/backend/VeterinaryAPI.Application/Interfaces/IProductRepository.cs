using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Domain.Entities;

namespace VeterinaryAPI.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductListResponse> GetActiveProductsAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default);
            
        Task<ProductListResponse> GetDangerousDrugsAsync(
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default);
            
        Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> UpdateDescriptionAsync(int id, string description, CancellationToken cancellationToken = default);
    }
} 
