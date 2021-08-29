using Microsoft.Data.SqlClient;
using Shop.Brokers.Storages;
using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
using System;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;

        public ProductService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public async ValueTask<Product> AddProductAsync(Product product)
        {
            try
            {
                ValidateProductOnCreate(product);
                return await this.storageBroker.InsertProductAsync(product);
            }
            catch(NullProductException nullProductException)
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
            catch(SqlException sqlException)
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
