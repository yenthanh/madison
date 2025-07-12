using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? InactiveDate { get; set; }
        public int OrganisationId { get; set; }
        [StringLength(50)]
        public string? ProductCode { get; set; }
        [StringLength(200)]
        public string? ProductDescription { get; set; }
        [StringLength(50)]
        public string? SupplierProductCode { get; set; }
        [Column(TypeName = "money")]
        public decimal? SupplierPrice { get; set; }
        [StringLength(50)]
        public string? ManufacturerCode { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? BoughtInQuantity { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? SoldInQuantity { get; set; }
        [Column(TypeName = "decimal(9,3)")]
        public decimal? SoldInMarkup { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? WholesaleDiscount { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? ManufacturerDiscount { get; set; }
        public bool? UseWholesaleDiscount { get; set; }
        public bool? UseManufacturerDiscount { get; set; }
        public bool? Dangerous { get; set; }
        public bool? Neutering { get; set; }
        public bool? Euthanasia { get; set; }
        public bool? BookWithoutServiceFee { get; set; }
        public bool? PrescriptionOnly { get; set; }
    }
} 
