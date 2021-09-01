using Domain.Models.Products;
using FluentAssertions;
using Moq;
using Xunit;

namespace UnitTests.Services.Foundations.Products
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

            this.storageMock.Setup(
                broker => broker.InsertProductAsync(inputProduct))
                .ReturnsAsync(storageProduct);

            // When
            Product actualProduct =
                await this.productService.AddProductAsync(inputProduct);

            // Then
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            this.storageMock.Verify(
                broker => broker.InsertProductAsync(storageProduct),
                times: Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public void ShouldRetrieveAllProducts()
        {
            // Given
            IQueryable<Product> randomProduct = CreateRandomProducts();
            IQueryable<Product> storageProducts = randomProduct;
            IQueryable<Product> expectedProducts = storageProducts;

            this.storageMock.Setup(broker =>
                broker.SelectAllProducts())
                .Returns(storageProducts);

            // When
            IQueryable<Product> actualProducts =
                this.productService.RetrieveAllProducts();

            // Then 
            actualProducts.Should().BeEquivalentTo(expectedProducts);

            this.storageMock.Verify(broker =>
                broker.SelectAllProducts(), Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }

    }
}
