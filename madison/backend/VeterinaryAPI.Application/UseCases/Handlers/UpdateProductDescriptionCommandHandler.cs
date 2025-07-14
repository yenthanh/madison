using MediatR;
using VeterinaryAPI.Application.Interfaces;
using VeterinaryAPI.Application.UseCases.Commands;

namespace VeterinaryAPI.Application.UseCases.Handlers
{
    public class UpdateProductDescriptionCommandHandler : IRequestHandler<UpdateProductDescriptionCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductDescriptionCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return false;

            if (!product.CanBeUpdated)
                return false;

            product.UpdateDescription(request.Description);
            await _productRepository.UpdateAsync(product);
            
            return true;
        }
    }
} 
