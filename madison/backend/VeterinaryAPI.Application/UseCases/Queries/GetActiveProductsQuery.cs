using MediatR;
using VeterinaryAPI.Application.DTOs;

namespace VeterinaryAPI.Application.UseCases.Queries
{
    public class GetActiveProductsQuery : IRequest<ProductListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetDangerousDrugsQuery : IRequest<ProductListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public int ProductId { get; set; }
    }
} 
