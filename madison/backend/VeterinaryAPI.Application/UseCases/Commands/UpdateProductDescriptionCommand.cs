using MediatR;
using VeterinaryAPI.Application.Interfaces;

namespace VeterinaryAPI.Application.UseCases.Commands
{
    public class UpdateProductDescriptionCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateProductDescriptionCommandHandler : IRequestHandler<UpdateProductDescriptionCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductDescriptionCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.UpdateDescriptionAsync(request.ProductId, request.Description, cancellationToken);
        }
    }

    public class CreateProductCommand : IRequest<int>
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal SupplierPrice { get; set; }
        public int OrganisationId { get; set; }
        public bool IsDangerous { get; set; }
    }

    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
} 
