using Microsoft.EntityFrameworkCore;
using Shop.Models.Products;

namespace Shop.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Product> Products { get; set; }
    }
}
