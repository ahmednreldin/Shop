using Application.Storages;
using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Application.Services.Fondations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorage Storage;

        public ProductService(IStorage Storage)
        {
            this.Storage = Storage;
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

        private static ProductValidationException CreateValidationException(Exception exception)
        {
            var productValidationException = new ProductValidationException(exception);
            return productValidationException;
        }
    }
}
