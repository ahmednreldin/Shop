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
        public void ShouldLogWarningOnRetrieveWhenProductIdIsInvalidAndLogIt()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.ProductId = Guid.Empty;

            var invalidProductException = new InvalidProductException(
                parameterName: nameof(inputProduct.ProductId),
                parameterValue: inputProduct.ProductId);

            var expectedValidationException =
                new ProductValidationException(invalidProductException);

            // When 
            ValueTask<Product> actualProductTask =
                   this.productService.RetrieveProductByIdAsync(inputProduct.ProductId);

            // Then
            Assert.ThrowsAsync<ProductValidationException>(() =>
               actualProductTask.AsTask());

            this.storageMock.Verify(storage =>
            storage.SelectProductByIdAsync(It.IsAny<Guid>()),
            Times.Never());

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
