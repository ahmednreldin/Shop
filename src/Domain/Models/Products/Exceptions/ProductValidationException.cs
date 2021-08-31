using System;

namespace Domain.Models.Products.Exceptions
{
    public class ProductValidationException : Exception
    {
        public ProductValidationException(Exception innerException) :
            base("invalid input , contact support", innerException)
        { }

    }
}
