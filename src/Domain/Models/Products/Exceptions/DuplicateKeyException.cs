using System;

namespace Shop.Web.Models.Products.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message) : base(message) { }
    }
}
