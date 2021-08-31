using Domain.Models.Products;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Storages
{
    public partial interface IStorage
    {
        ValueTask<Product> InsertProductAsync(Product product);
        IQueryable<Product> SelectAllProducts();
    }
}
