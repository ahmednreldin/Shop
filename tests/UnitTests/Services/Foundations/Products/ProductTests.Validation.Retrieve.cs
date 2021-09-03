using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace UnitTests.Services.Foundations.Products
{
    public partial class ProductTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnRetrieveWhenProductIdIsInvalid()
        {
            // Given
            Guid randomProductId = default;
            Guid inputProductId = randomProductId;

            // When 
            ValueTask<Product> retrieveProductByIdTask =
                   this.productService.RetrieveProductByIdAsync(inputProductId);

            // Then
            Assert.ThrowsAsync<ProductValidationException>(() =>
               retrieveProductByIdTask.AsTask());

            this.storageMock.Verify(storage =>
            storage.SelectProductByIdAsync(It.IsAny<Guid>()),
            Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowProductValidationExceptionOnRetrieveWhenStorageProductIsNullAsync()
        {
            // Given
            Guid randomProductId = Guid.NewGuid();
            Guid inputProductId = randomProductId;
            Product invalidStorageProduct = null;

            this.storageMock.Setup(storage =>
            storage.SelectProductByIdAsync(inputProductId)).ReturnsAsync(invalidStorageProduct);

            // When
            ValueTask<Product> retrieveProductByIdTask =
                this.productService.RetrieveProductByIdAsync(inputProductId);

            // Then
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                retrieveProductByIdTask.AsTask()
            );

            this.storageMock.Verify(storage =>
            storage.SelectProductByIdAsync(It.IsAny<Guid>()), Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldLogWarningOnRetrieveAllWhenProductsIsEmptyAndLogIt()
        {
            // Given 
            IQueryable<Product> emptyStorageProducts = new List<Product>().AsQueryable();
            IQueryable<Product> expectedProducts = emptyStorageProducts;

            this.storageMock.Setup(broker =>
            broker.SelectAllProducts())
                .Returns(expectedProducts);

            // When 
            IQueryable<Product> actualProducts =
                this.productService.RetrieveAllProducts();

            // Then 
            actualProducts.Should().BeEquivalentTo(emptyStorageProducts);

            this.loggingMock.Verify(logging =>
            logging.LogWarning("No Products found in storage."),
            Times.Once());

            this.storageMock.Verify(broker =>
            broker.SelectAllProducts()
            , Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }
    }
}
