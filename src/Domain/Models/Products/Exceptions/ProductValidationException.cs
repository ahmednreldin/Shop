using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Products.Exceptions
{
    public class ProductValidationException : Exception
    {
        public ProductValidationException(Exception innerException) :
            base("invalid input , contact support", innerException) { }
                
    }
}
