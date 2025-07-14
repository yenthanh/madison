using System.ComponentModel.DataAnnotations;

namespace VeterinaryAPI.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public DateTime? DeleteDate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public Guid? UpdatedBy { get; private set; }
        public DateTime? InactiveDate { get; private set; }
        public int OrganisationId { get; private set; }
        
        [StringLength(50)]
        public string? ProductCode { get; private set; }
        
        [StringLength(200)]
        public string? ProductDescription { get; private set; }
        
        [StringLength(50)]
        public string? SupplierProductCode { get; private set; }
        
        public decimal? SupplierPrice { get; private set; }
        
        [StringLength(50)]
        public string? ManufacturerCode { get; private set; }
        
        public decimal? BoughtInQuantity { get; private set; }
        public decimal? SoldInQuantity { get; private set; }
        public decimal? SoldInMarkup { get; private set; }
        public decimal? WholesaleDiscount { get; private set; }
        public decimal? ManufacturerDiscount { get; private set; }
        public bool? UseWholesaleDiscount { get; private set; }
        public bool? UseManufacturerDiscount { get; private set; }
        public bool? Dangerous { get; private set; }
        public bool? Neutering { get; private set; }
        public bool? Euthanasia { get; private set; }
        public bool? BookWithoutServiceFee { get; private set; }
        public bool? PrescriptionOnly { get; private set; }

        // Domain logic properties
        public bool IsActive => InactiveDate == null;
        public bool IsDeleted => DeleteDate != null;
        public bool IsDangerousDrug => Dangerous == true;

        // Constructor for EF Core
        private Product() { }

        // Domain constructor
        public Product(string productCode, string productDescription, decimal supplierPrice, int organisationId, Guid? createdBy = null)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code cannot be empty", nameof(productCode));
            
            if (string.IsNullOrWhiteSpace(productDescription))
                throw new ArgumentException("Product description cannot be empty", nameof(productDescription));
            
            if (supplierPrice < 0)
                throw new ArgumentException("Supplier price cannot be negative", nameof(supplierPrice));

            ProductCode = productCode;
            ProductDescription = productDescription;
            SupplierPrice = supplierPrice;
            OrganisationId = organisationId;
            CreatedBy = createdBy;
            CreateDate = DateTime.UtcNow;
        }

        // Domain methods
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                throw new ArgumentException("Description cannot be empty", nameof(newDescription));
            
            if (newDescription.Length > 200)
                throw new ArgumentException("Description cannot exceed 200 characters", nameof(newDescription));

            ProductDescription = newDescription;
            UpdateDate = DateTime.UtcNow;
        }

        public void MarkAsInactive(Guid? updatedBy = null)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot mark deleted product as inactive");

            InactiveDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        public void MarkAsActive(Guid? updatedBy = null)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot mark deleted product as active");

            InactiveDate = null;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        public void MarkAsDangerous(Guid? updatedBy = null)
        {
            Dangerous = true;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        public void MarkAsNonDangerous(Guid? updatedBy = null)
        {
            Dangerous = false;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        public void SoftDelete(Guid? updatedBy = null)
        {
            DeleteDate = DateTime.UtcNow;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        public void Restore(Guid? updatedBy = null)
        {
            DeleteDate = null;
            UpdatedBy = updatedBy;
            UpdateDate = DateTime.UtcNow;
        }

        // Business rules validation
        public bool CanBeUpdated => !IsDeleted;
        public bool CanBeDeleted => !IsDeleted;
        public bool CanBeActivated => IsDeleted == false;
        public bool CanBeDeactivated => IsActive && !IsDeleted;
    }
} 
