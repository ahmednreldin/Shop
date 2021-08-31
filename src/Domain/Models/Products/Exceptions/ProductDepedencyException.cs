using System;

namespace Domain.Models.Products.Exceptions
{
    public class ProductDepedencyException : Exception
    {
        public ProductDepedencyException(Exception innerException) :
            base("Service dependency error occured , contact support", innerException)
        { }

    }
}
