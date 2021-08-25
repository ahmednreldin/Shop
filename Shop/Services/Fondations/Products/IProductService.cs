using Shop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
    }
}
