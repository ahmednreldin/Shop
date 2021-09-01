using Domain.Models.Products;
using FluentAssertions;
using Moq;
using Xunit;

namespace UnitTests.Services.Foundations.Products
{
    public partial class ProductTests
    {
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
