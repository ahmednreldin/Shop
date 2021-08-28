using FluentAssertions;
using Moq;
using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
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
            await Assert.ThrowsAsync<ProductValidationException>(() =>
               addProductTask.AsTask()
            );

            this.storageBrokerMock.Verify(broker =>
            broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionOnAddWhenIdIsInvalidAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Id = default;

            var invalidProductInputException = 
                new InvalidProductException (
                    parameterName : nameof(inputProduct.Id),
                    parameterValue : inputProduct.Id
                    );
            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(inputProduct);


            // Then
           await Assert.ThrowsAsync<ProductValidationException>(() =>
               addProductTask.AsTask()
            );

            this.storageBrokerMock.Verify(broker =>
                broker.InsertProductAsync(It.IsAny<Product>()),
                Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public void ShouldThrowValidationExceptionWhenNameIsInvalid()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Name = default;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.Name),
                parameterValue: inputProduct.Name);
            
            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask = 
                this.productService.AddProductAsync(inputProduct);

            // Then 
            Assert.ThrowsAsync<ProductValidationException>(() =>
                addProductTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());
            
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
  
        [Fact]
        public async void ShouldThrowValidationExceptionWhenDescriptionIsInvalidAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Description = default;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.Description),
                parameterValue: inputProduct.Description);

            var expectedProductValidationException = 
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask = 
                this.productService.AddProductAsync(inputProduct);

            // Then 
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                     addProductTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertProductAsync(inputProduct), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionWhenImageUrlIsInvalid()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.ImageUrl = default;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.Description),
                parameterValue: inputProduct.Description);

            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(inputProduct);

            // Then 
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                     addProductTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertProductAsync(inputProduct), Times.Never());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionWhenSaleryIsInvalid()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Salery = default;

           var invalidProductInputException = new InvalidProductException(
               parameterName: nameof(inputProduct.Salery),
               parameterValue: inputProduct.Salery);

            var expectedProductValidationException = 
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                 this.productService.AddProductAsync(inputProduct);

           // Then
           await Assert.ThrowsAsync<ProductValidationException>(() =>
            addProductTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertProductAsync(inputProduct), Times.Never());
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionWhenProductIsAlreadyExist()
        {

        }

    }
}
