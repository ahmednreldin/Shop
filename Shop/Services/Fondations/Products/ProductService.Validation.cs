using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
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

                case { } when IsInvalid(product.Description):
                    throw new InvalidProductException(
                        parameterName: nameof(product.Description),
                        parameterValue: product.Description);
                case { } when IsInvalid(product.ImageUrl):
                    throw new InvalidProductException(
                        parameterName: nameof(product.ImageUrl),
                        parameterValue: product.ImageUrl);
                       
            }
        }

        private bool IsInvalid(string input) => string.IsNullOrEmpty(input);

        private bool IsInvalid(Guid id) => id == Guid.Empty;
    }
}
