using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Products.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string parameterName,object parameterValue)
        {

        }
    }
}
