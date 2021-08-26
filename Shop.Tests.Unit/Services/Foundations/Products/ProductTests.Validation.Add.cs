using FluentAssertions;
using Moq;
using Shop.Models.Products;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.Unit.Services.Foundations.Products
{
    public partial class ProductTests
    {

        [Fact]
       public async void ShouldThrowValidationExceptionOnAddWhenProductIsNullAsync()
        {
            // Given 
            Product nullProduct = null;

            var nullProductException = new NullProductException();

            var expectedProductValidationException =
                new ProductValidationException(nullProductException);

            // When
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(nullProduct);

            // Then
            Assert.ThrowsAsync<ProductValidationException>(() =>
               addProductTask.AsTask()
            );

            this.storageBrokerMock.Verify(broker =>
            broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
