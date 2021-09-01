using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using Moq;
using Xunit;

namespace UnitTests.Services.Foundations.Products
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

            this.storageMock.Verify(broker =>
            broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionOnAddWhenProductIdIsInvalidAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.ProductId = default;

            var invalidProductInputException =
                new InvalidProductException(
                    parameterName: nameof(inputProduct.ProductId),
                    parameterValue: inputProduct.ProductId
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

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(It.IsAny<Product>()),
                Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ShouldThrowValidationExceptionWhenNameIsInvalid(string invalidProductName)
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Name = invalidProductName;

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

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionWhenDescriptionIsInvalidAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.ShortDescription = default;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.ShortDescription),
                parameterValue: inputProduct.ShortDescription);

            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(inputProduct);

            // Then 
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                     addProductTask.AsTask());

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(inputProduct), Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionWhenImageUrlIsInvalid()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Picture = default;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.Picture),
                parameterValue: inputProduct.Picture);

            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(inputProduct);

            // Then 
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                  addProductTask.AsTask());

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(inputProduct), Times.Never());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Theory]
        [InlineData(double.MaxValue + double.MaxValue)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        public async void ShouldThrowValidationExceptionWhenSaleryIsInvalid(
            double invalidProductSalery)
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            inputProduct.Price = invalidProductSalery;

            var invalidProductInputException = new InvalidProductException(
                parameterName: nameof(inputProduct.Price),
                parameterValue: inputProduct.Price);

            var expectedProductValidationException =
                new ProductValidationException(invalidProductInputException);

            // When
            ValueTask<Product> addProductTask =
                 this.productService.AddProductAsync(inputProduct);

            // Then
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                 addProductTask.AsTask());

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(It.IsAny<Product>()), Times.Never());
            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldThrowValidationExceptionWhenProductIsAlreadyExist()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product alreadyExistProduct = randomProduct;
            string randomMassage = GetRandomMessage();
            string exceptionMessage = randomMassage;
            var duplicatekeyException = new DuplicateKeyException(exceptionMessage);

            var alreadyExistProductException =
                new AlreadyExistsProductException(duplicatekeyException);

            var excpectedProductValidationException =
                new ProductValidationException(alreadyExistProductException);

            this.storageMock.Setup(broker =>
                broker.InsertProductAsync(alreadyExistProduct))
                .ThrowsAsync(duplicatekeyException);

            // When 
            ValueTask<Product> addProductTask =
                this.productService.AddProductAsync(alreadyExistProduct);

            // Then 
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                addProductTask.AsTask());

            this.storageMock.Verify(broker =>
                broker.InsertProductAsync(alreadyExistProduct),
                Times.Once());

            this.storageMock.VerifyNoOtherCalls();

        }

    }
}
