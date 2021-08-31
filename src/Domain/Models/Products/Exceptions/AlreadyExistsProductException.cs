using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Products.Exceptions
{
    public class AlreadyExistsProductException : Exception
    {
        public AlreadyExistsProductException(Exception innerException) :
            base(innerException.Message, innerException) { }
    }
}
