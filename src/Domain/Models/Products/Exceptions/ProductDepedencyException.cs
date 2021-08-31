using System;

namespace Shop.Web.Models.Products.Exceptions
{
    public class ProductDepedencyException : Exception
    {
        public ProductDepedencyException(Exception innerException) :
            base("Service dependency error occured , contact support", innerException)
        { }

    }
}
