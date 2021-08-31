using System;

namespace Domain.Models.Products.Exceptions
{
    public class AlreadyExistsProductException : Exception
    {
        public AlreadyExistsProductException(Exception innerException) :
            base(innerException.Message, innerException)
        { }
    }
}
