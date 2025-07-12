using VeterinaryAPI.Models;

namespace VeterinaryAPI.Services
{
    public interface IProductService
    {
        Task<ProductListResponse> GetAllActiveProductsAsync(int pageNumber = 1, int pageSize = 20);
        Task<ProductListResponse> GetDangerousDrugsAsync(int pageNumber = 1, int pageSize = 20);
        Task<bool> UpdateProductDescriptionAsync(int productId, string description);
        Task<ProductDto?> GetProductByIdAsync(int productId);
    }
} 
