using System;

namespace Domain.Models.Products.Exceptions
{
    public class NullProductException : Exception
    {
        public NullProductException() : base("the product is null") { }
    }
}
