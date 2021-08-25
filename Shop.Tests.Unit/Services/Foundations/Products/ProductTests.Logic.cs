using FluentAssertions;
using Moq;
using Shop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.Unit.Services.Foundations.Products
{
    public partial class ProductTests
    {
        [Fact]
        public async Task ShouldAddProductAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            Product storageProduct = inputProduct;
            Product expectedProduct = storageProduct;

            this.storageBrokerMock.Setup(
                broker => broker.InsertProductAsync(inputProduct))
                .ReturnsAsync(storageProduct);

            // When
            Product actualProduct =
                await this.productService.AddProductAsync(inputProduct);

            // Then
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            this.storageBrokerMock.Verify(
                broker => broker.InsertProductAsync(storageProduct),
                times: Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
