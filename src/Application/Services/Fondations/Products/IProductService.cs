using Domain.Models.Products;
using System.Threading.Tasks;

namespace Application.Services.Fondations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
    }
}
