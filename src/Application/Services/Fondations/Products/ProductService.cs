﻿using Application.Loggins;
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
                if (productId == Guid.Empty)
                {
                    throw new InvalidProductException(
                        parameterName: nameof(productId),
                        parameterValue: productId);
                }

                Product product = await this.Storage.SelectProductByIdAsync(productId);

                if (product == null)
                    throw new NotFoundProductException(productId);


                return product;
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

        private static ProductValidationException CreateValidationException(Exception exception)
        {
            var productValidationException = new ProductValidationException(exception);
            return productValidationException;
        }
    }
}
