using MediatR;
using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Application.Interfaces;
using VeterinaryAPI.Application.UseCases.Queries;

namespace VeterinaryAPI.Application.UseCases.Handlers
{
    public class GetDangerousDrugsQueryHandler : IRequestHandler<GetDangerousDrugsQuery, ProductListResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetDangerousDrugsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductListResponse> Handle(GetDangerousDrugsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetDangerousDrugsAsync(request.PageNumber, request.PageSize);
            var totalCount = await _productRepository.GetDangerousDrugsCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var productDtos = products.Select(MapToDto).ToList();

            return new ProductListResponse
            {
                Products = productDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages
            };
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
