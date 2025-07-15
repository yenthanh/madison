using VeterinaryAPI.Application.DTOs;
using VeterinaryAPI.Application.Interfaces;
using MediatR;

namespace VeterinaryAPI.Application.UseCases.Queries
{
    public class GetDangerousDrugsQuery : IRequest<ProductListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetDangerousDrugsQueryHandler : IRequestHandler<GetDangerousDrugsQuery, ProductListResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetDangerousDrugsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductListResponse> Handle(GetDangerousDrugsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetDangerousDrugsAsync(
                request.PageNumber, 
                request.PageSize, 
                cancellationToken: cancellationToken);
            return products;
        }
    }
} 
