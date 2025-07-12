using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Models;

namespace VeterinaryAPI.Data
{
    public class MadisonDbContext : DbContext
    {
        public MadisonDbContext(DbContextOptions<MadisonDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.CreateDate);
                entity.Property(e => e.UpdateDate);
                entity.Property(e => e.DeleteDate);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.UpdatedBy);
                entity.Property(e => e.InactiveDate);
                entity.Property(e => e.OrganisationId);
                entity.Property(e => e.ProductCode).HasMaxLength(50);
                entity.Property(e => e.ProductDescription).HasMaxLength(200);
                entity.Property(e => e.SupplierProductCode).HasMaxLength(50);
                entity.Property(e => e.SupplierPrice).HasColumnType("money");
                entity.Property(e => e.ManufacturerCode).HasMaxLength(50);
                entity.Property(e => e.BoughtInQuantity).HasColumnType("decimal(9,2)");
                entity.Property(e => e.SoldInQuantity).HasColumnType("decimal(9,2)");
                entity.Property(e => e.SoldInMarkup).HasColumnType("decimal(9,3)");
                entity.Property(e => e.WholesaleDiscount).HasColumnType("decimal(9,2)");
                entity.Property(e => e.ManufacturerDiscount).HasColumnType("decimal(9,2)");
                entity.Property(e => e.UseWholesaleDiscount);
                entity.Property(e => e.UseManufacturerDiscount);
                entity.Property(e => e.Dangerous);
                entity.Property(e => e.Neutering);
                entity.Property(e => e.Euthanasia);
                entity.Property(e => e.BookWithoutServiceFee);
                entity.Property(e => e.PrescriptionOnly);
            });
        }
    }
} 
