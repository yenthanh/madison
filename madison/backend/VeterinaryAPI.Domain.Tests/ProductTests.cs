using System;
using VeterinaryAPI.Domain.Entities;
using Xunit;

namespace VeterinaryAPI.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void UpdateDescription_ShouldUpdate_WhenValid()
        {
            var product = new Product("P001", "Old desc", 10, 1);
            product.UpdateDescription("New desc");
            Assert.Equal("New desc", product.ProductDescription);
        }

        [Fact]
        public void UpdateDescription_ShouldThrow_WhenEmpty()
        {
            var product = new Product("P001", "Old desc", 10, 1);
            Assert.Throws<ArgumentException>(() => product.UpdateDescription(""));
        }

        [Fact]
        public void MarkAsInactive_ShouldSetInactiveDate()
        {
            var product = new Product("P001", "desc", 10, 1);
            product.MarkAsInactive();
            Assert.False(product.IsActive);
        }

        [Fact]
        public void MarkAsActive_ShouldClearInactiveDate()
        {
            var product = new Product("P001", "desc", 10, 1);
            product.MarkAsInactive();
            product.MarkAsActive();
            Assert.True(product.IsActive);
        }

        [Fact]
        public void MarkAsDangerous_ShouldSetDangerous()
        {
            var product = new Product("P001", "desc", 10, 1);
            product.MarkAsDangerous();
            Assert.True(product.IsDangerousDrug);
        }

        [Fact]
        public void MarkAsNonDangerous_ShouldUnsetDangerous()
        {
            var product = new Product("P001", "desc", 10, 1);
            product.MarkAsDangerous();
            product.MarkAsNonDangerous();
            Assert.False(product.IsDangerousDrug);
        }
    }
} 
