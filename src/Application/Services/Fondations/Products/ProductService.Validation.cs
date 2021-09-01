using Domain.Models.Products;
using Domain.Models.Products.Exceptions;

namespace Application.Services.Fondations.Products
{
    public partial class ProductService
    {
        public void ValidateStorageProducts(IQueryable<Product> products)
        {
            if (!products.Any())
            {
                logging.LogWarning("No Products found in storage.");
            }
        }
        public void ValidateProductOnCreate(Product product)
        {
            switch (product)
            {
                case null:
                    throw new NullProductException();
                case { } when IsInvalid(product.ProductId):
                    throw new InvalidProductException(
                        parameterName: nameof(product.ProductId),
                        parameterValue: product.ProductId);

                case { } when IsInvalid(product.Name):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Name),
                        parameterValue: product.Name);

                case { } when IsInvalid(product.ShortDescription):
                    throw new InvalidProductException(
                        parameterName: nameof(product.ShortDescription),
                        parameterValue: product.ShortDescription);

                case { } when IsInvalid(product.Picture):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Picture),
                        parameterValue: product.Picture);

                case { } when IsInvalid(product.Price):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Price),
                        parameterValue: product.Price);
            }
        }

        private bool IsInvalid(double input) => !double.IsNormal(input);
        private bool IsInvalid(string input) => string.IsNullOrWhiteSpace(input);
        private bool IsInvalid(Guid id) => id == Guid.Empty;
    }
}
