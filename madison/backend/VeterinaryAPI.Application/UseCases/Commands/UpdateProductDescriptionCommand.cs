using MediatR;

namespace VeterinaryAPI.Application.UseCases.Commands
{
    public class UpdateProductDescriptionCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
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
