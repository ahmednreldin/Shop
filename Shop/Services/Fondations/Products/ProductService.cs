using Shop.Brokers.Storages;
using Shop.Models.Products;
using Shop.Web.Models.Products.Exceptions;
using System.Threading.Tasks;

namespace Shop.Web.Services.Fondations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;

        public ProductService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public async ValueTask<Product> AddProductAsync(Product product)
        {
            try
            {
                ValidateProductOnCreate(product);
                return await this.storageBroker.InsertProductAsync(product);
            }
            catch(NullProductException nullProductException)
            {
                throw new ProductValidationException(nullProductException);
            }

         
        }
           
    }
}
