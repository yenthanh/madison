using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Domain.Entities;

namespace VeterinaryAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.ProductId);
                
                // Configure properties
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
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

                // Configure indexes
                entity.HasIndex(e => e.ProductCode);
                entity.HasIndex(e => e.CreateDate);
                entity.HasIndex(e => e.DeleteDate);
                entity.HasIndex(e => e.InactiveDate);
                entity.HasIndex(e => e.Dangerous);
            });
        }
    }
} 
