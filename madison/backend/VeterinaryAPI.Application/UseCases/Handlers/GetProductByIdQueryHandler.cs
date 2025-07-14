using MediatR;
using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Application.Interfaces;
using VeterinaryAPI.Application.UseCases.Queries;

namespace VeterinaryAPI.Application.UseCases.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            return product != null ? MapToDto(product) : null;
        }

        private static ProductDto MapToDto(VeterinaryAPI.Domain.Entities.Product product)
        {
            return new ProductDto
            {
                Id = product.ProductId,
                Name = product.ProductCode ?? "No name",
                Description = product.ProductDescription ?? "No description",
                Category = "Veterinary",
                Price = product.SupplierPrice ?? 0,
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                IsDangerousDrug = product.IsDangerousDrug,
                CreatedAt = product.CreateDate,
                UpdatedAt = product.UpdateDate
            };
        }
    }
} 
