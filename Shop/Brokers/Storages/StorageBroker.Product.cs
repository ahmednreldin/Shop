using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.Models.Products;
using System.Threading.Tasks;

namespace Shop.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Product> Products { get; set; }

        public async ValueTask<Product> InsertProduct(Product product)
        {
            EntityEntry<Product> entityEntry = await this.Products.AddAsync(product);
            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
