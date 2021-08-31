using Moq;
using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
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
        public async Task ShouldThrowDependencyExceptionOnAddWhenSqlExceptionOccursAsync() 
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            var sqlException = GetSqlException();

            var expectedProductDependencyException =
                new ProductDepedencyException(sqlException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertProductAsync(inputProduct)).ThrowsAsync(sqlException);

            // When
            ValueTask<Product> productAddTask = 
                this.productService.AddProductAsync(inputProduct);

            // Then
            await Assert.ThrowsAsync<ProductDepedencyException>(() =>
               productAddTask.AsTask()
            );

            this.storageBrokerMock.Verify( broker => 
                broker.InsertProductAsync(It.IsAny<Product>())
               ,Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
