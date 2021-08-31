using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models.Products.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message ) : base(message) { }
    }
}
