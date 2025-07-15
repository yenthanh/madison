using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Application.Interfaces;
using MediatR;

namespace VeterinaryAPI.Application.UseCases.Queries
{
    public class GetActiveProductsQuery : IRequest<ProductListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetActiveProductsQueryHandler : IRequestHandler<GetActiveProductsQuery, ProductListResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetActiveProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductListResponse> Handle(GetActiveProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetActiveProductsAsync(
                request.PageNumber, 
                request.PageSize, 
                cancellationToken: cancellationToken);
            return products;
        }
    }

    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        }
    }
} 
