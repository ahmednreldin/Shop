using System;

namespace Domain.Models.Products.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string parameterName, object parameterValue)
        : base($"Invalid Product " +
             $"Parameter Name : {parameterName}" +
             $"Parameter Value:{ parameterValue}")
        { }
    }
}
