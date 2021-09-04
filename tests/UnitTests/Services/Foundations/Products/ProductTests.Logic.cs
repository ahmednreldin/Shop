using Domain.Models.Products;
using FluentAssertions;
using Force.DeepCloner;
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
        [Fact]
        public async void ShouldRetrieveProductById()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product storageProduct = randomProduct;
            Product expectedProduct = storageProduct;
            Guid productId = expectedProduct.ProductId;

            this.storageMock.Setup(storage =>
            storage.SelectProductByIdAsync(productId))
                .ReturnsAsync(storageProduct);

            // When
            Product actualProduct =
               await this.productService.RetrieveProductByIdAsync(productId);

            // Then 
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            this.storageMock.Verify(storage =>
            storage.SelectProductByIdAsync(It.IsAny<Guid>()),
              Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void ShouldModifyProductAsync()
        {
            // Given
            Product randomProduct = CreateRandomProduct();
            Product inputProduct = randomProduct;
            Product afterUpdateStorageProduct = inputProduct;
            Product expectedProduct = afterUpdateStorageProduct;
            Product beforeUpdateStorageProduct = randomProduct.DeepClone();
            inputProduct.Name = "UpdateProduct";
            Guid productId = inputProduct.ProductId;


            this.storageMock.Setup(storage =>
                storage.SelectProductByIdAsync(productId))
                .ReturnsAsync(beforeUpdateStorageProduct);

            this.storageMock.Setup(storage =>
            storage.UpdateProductAsync(inputProduct))
                .ReturnsAsync(afterUpdateStorageProduct);

            // When
            Product actualProduct =
              await this.productService.ModifyProductAsync(inputProduct);

            // Then 
            actualProduct.Should().BeEquivalentTo(expectedProduct);

            this.storageMock.Verify(storage =>
                storage.SelectProductByIdAsync(productId), Times.Once());

            this.storageMock.Verify(storage =>
            storage.UpdateProductAsync(inputProduct),
            Times.Once());

            this.storageMock.VerifyNoOtherCalls();
        }

    }
}
