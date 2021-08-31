using Domain.Models.Products;
using Domain.Models.Products.Exceptions;
using System;

namespace Application.Services.Fondations.Products
{
    public partial class ProductService
    {
        public void ValidateProductOnCreate(Product product)
        {
            switch (product)
            {
                case null:
                    throw new NullProductException();
                case { } when IsInvalid(product.Id):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Id),
                        parameterValue: product.Id);

                case { } when IsInvalid(product.Name):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Id),
                        parameterValue: product.Name);

                case { } when IsInvalid(product.Description):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Description),
                        parameterValue: product.Description);

                case { } when IsInvalid(product.ImageUrl):
                    throw new InvalidProductException(
                        parameterName: nameof(product.ImageUrl),
                        parameterValue: product.ImageUrl);

                case { } when IsInvalid(product.Salery):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Salery),
                        parameterValue: product.Salery);
            }
        }

        private bool IsInvalid(double input) => !double.IsNormal(input);
        private bool IsInvalid(string input) => string.IsNullOrWhiteSpace(input);
        private bool IsInvalid(Guid id) => id == Guid.Empty;
    }
}
