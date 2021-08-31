using System;

namespace Shop.Web.Models.Products.Exceptions
{
    public class ProductValidationException : Exception
    {
        public ProductValidationException(Exception innerException) :
            base("invalid input , contact support", innerException)
        { }

    }
}
