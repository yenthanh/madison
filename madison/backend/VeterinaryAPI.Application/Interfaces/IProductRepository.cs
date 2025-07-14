using VeterinaryAPI.Domain.Entities;

namespace VeterinaryAPI.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllActiveAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetDangerousDrugsAsync(int pageNumber, int pageSize);
        Task<int> GetActiveCountAsync();
        Task<int> GetDangerousDrugsCountAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
} 
