using Shop.Models.Products;
using System.Threading.Tasks;

namespace Shop.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Product> InsertProduct(Product product);  
    }
}
