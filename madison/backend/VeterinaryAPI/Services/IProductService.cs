using VeterinaryAPI.Models;

namespace VeterinaryAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllActiveProductsAsync();
        Task<IEnumerable<Product>> GetDangerousDrugsAsync();
        Task<bool> UpdateProductDescriptionAsync(int productId, string description);
        Task<Product?> GetProductByIdAsync(int productId);
    }
} 
