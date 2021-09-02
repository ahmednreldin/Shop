using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Storages
{
    public partial class Storage
    {
        public DbSet<Product> Products { get; set; }

        public async ValueTask<Product> InsertProductAsync(Product product)
        {
            EntityEntry<Product> entityEntry = await this.Products.AddAsync(product);
            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
        public IQueryable<Product> SelectAllProducts() =>
            this.Products.AsQueryable();

        public ValueTask<Product> SelectProductByIdAsync(Guid productId) =>
            this.Products.FindAsync(productId);
    }
}
