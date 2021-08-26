using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
{
    public partial class ProductService
    {
        public void ValidateProductOnCreate(Product product)
        {
            switch(product)
            {
                case null:
                    throw new NullProductException();
            }
        }
    }
}
