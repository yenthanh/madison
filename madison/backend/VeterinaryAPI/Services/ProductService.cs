using VeterinaryAPI.Models;

namespace VeterinaryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            // Initialize with sample data
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Amoxicillin 250mg",
                    Description = "Antibiotic for bacterial infections in animals",
                    Category = "Antibiotics",
                    Price = 25.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsDangerousDrug = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Product
                {
                    Id = 2,
                    Name = "Morphine Sulfate 10mg",
                    Description = "Strong pain reliever for severe pain management",
                    Category = "Pain Management",
                    Price = 45.50m,
                    IsActive = true,
                    IsDeleted = false,
                    IsDangerousDrug = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Product
                {
                    Id = 3,
                    Name = "Ivermectin 1%",
                    Description = "Anti-parasitic medication for internal and external parasites",
                    Category = "Anti-parasitic",
                    Price = 15.75m,
                    IsActive = true,
                    IsDeleted = false,
                    IsDangerousDrug = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Product
                {
                    Id = 4,
                    Name = "Ketamine 100mg/ml",
                    Description = "Anesthetic and pain management drug",
                    Category = "Anesthesia",
                    Price = 120.00m,
                    IsActive = true,
                    IsDeleted = false,
                    IsDangerousDrug = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Product
                {
                    Id = 5,
                    Name = "Vitamin B Complex",
                    Description = "Vitamin supplement for general health",
                    Category = "Supplements",
                    Price = 12.99m,
                    IsActive = false,
                    IsDeleted = false,
                    IsDangerousDrug = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new Product
                {
                    Id = 6,
                    Name = "Expired Product",
                    Description = "This product has been discontinued",
                    Category = "Discontinued",
                    Price = 0.00m,
                    IsActive = false,
                    IsDeleted = true,
                    IsDangerousDrug = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                }
            };
        }

        public async Task<IEnumerable<Product>> GetAllActiveProductsAsync()
        {
            return await Task.FromResult(_products
                .Where(p => p.IsActive && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToList());
        }

        public async Task<IEnumerable<Product>> GetDangerousDrugsAsync()
        {
            return await Task.FromResult(_products
                .Where(p => p.IsDangerousDrug && p.IsActive && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt)
                .ToList());
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return false;

            product.Description = description;
            product.UpdatedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == productId));
        }
    }
} 
