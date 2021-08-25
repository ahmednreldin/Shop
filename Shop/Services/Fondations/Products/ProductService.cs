using Shop.Brokers.Storages;
using Shop.Models.Products;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;

        public ProductService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public  async ValueTask<Product> AddProductAsync(Product product) =>
            await this.storageBroker.InsertProductAsync(product);
           
    }
}
