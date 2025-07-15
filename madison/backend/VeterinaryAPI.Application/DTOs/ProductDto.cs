namespace VeterinaryAPI.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDangerousDrug { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string? UpdatedAt { get; set; }
    }

    public class ProductListResponse
    {
        public List<ProductDto> Products { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class UpdateProductDescriptionDto
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class CreateProductDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal SupplierPrice { get; set; }
        public int OrganisationId { get; set; }
        public bool IsDangerous { get; set; }
    }
} 
