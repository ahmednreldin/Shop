using Shop.Models.Products;
using System.Threading.Tasks;

namespace Shop.Brokers.Storages
{
    public partial interface IStorage
    {
        ValueTask<Product> InsertProductAsync(Product product);
    }
}
