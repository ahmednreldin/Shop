using Moq;
using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services.Foundations.Products
{
    public partial class ProductTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddWhenSqlExceptionOccursAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            var sqlException = GetSqlException();

            var expectedProductDependencyException =
                new ProductDepedencyException(sqlException);

            this.storageMock.Setup(broker =>
                broker.InsertProductAsync(inputProduct)).ThrowsAsync(sqlException);

            // When
            ValueTask<Product> productAddTask =
                this.productService.AddProductAsync(inputProduct);

            // Then
            await Assert.ThrowsAsync<ProductDepedencyException>(() =>
               productAddTask.AsTask()
            );

            this.storageMock.Verify(broker =>
               broker.InsertProductAsync(It.IsAny<Product>())
               , Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }
    }
}
