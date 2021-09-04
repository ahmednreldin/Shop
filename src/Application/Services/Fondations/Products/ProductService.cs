using Application.Loggins;
using Application.Storages;
using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using System.Data.SqlClient;

namespace Application.Services.Fondations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorage Storage;
        private readonly ILogging logging;

        public ProductService(IStorage Storage, ILogging logging)
        {
            this.Storage = Storage;
            this.logging = logging;
        }
        public async ValueTask<Product> AddProductAsync(Product product)
        {
            try
            {
                ValidateProductOnCreate(product);
                return await this.Storage.InsertProductAsync(product);
            }
            catch (NullProductException nullProductException)
            {
                throw CreateValidationException(nullProductException);
            }
            catch (InvalidProductException invalidProductException)
            {
                throw CreateValidationException(invalidProductException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsProductException =
                    new AlreadyExistsProductException(duplicateKeyException);
                throw CreateValidationException(alreadyExistsProductException);
            }
            catch (SqlException sqlException)
            {
                throw new ProductDepedencyException(sqlException);
            }

        }

        public async ValueTask<Product> RetrieveProductByIdAsync(Guid productId)
        {
            try
            {
                ValidateProductId(productId);

                Product storageProduct = await this.Storage.SelectProductByIdAsync(productId);

                ValidateStorageProduct(productId, storageProduct);

                return storageProduct;
            }
            catch (InvalidProductException invalidProductException)
            {
                throw CreateValidationException(invalidProductException);
            }
            catch (NotFoundProductException notFoundProductException)
            {
                throw CreateValidationException(notFoundProductException);
            }

        }

        public IQueryable<Product> RetrieveAllProducts()
        {
            IQueryable<Product> products = this.Storage.SelectAllProducts();
            ValidateStorageProducts(products);
            return products;
        }

        public async ValueTask<Product> ModifyProductAsync(Product product)
        {
            Product storageProduct =
                await this.Storage.SelectProductByIdAsync(product.ProductId);
            if (storageProduct == null)
                throw new NotFoundProductException(product.ProductId);

            return await this.Storage.UpdateProductAsync(product);
        }

        private static ProductValidationException CreateValidationException(Exception exception)
        {
            var productValidationException = new ProductValidationException(exception);
            return productValidationException;
        }
    }
}
