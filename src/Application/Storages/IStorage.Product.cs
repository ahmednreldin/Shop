using Domain.Models.Products;
using System.Threading.Tasks;

namespace Application.Storages
{
    public partial interface IStorage
    {
        ValueTask<Product> InsertProductAsync(Product product);
    }
}
