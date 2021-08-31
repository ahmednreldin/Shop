using System;

namespace Shop.Web.Models.Products.Exceptions
{
    public class NullProductException : Exception
    {
        public NullProductException() : base("the product is null") { }
    }
}
