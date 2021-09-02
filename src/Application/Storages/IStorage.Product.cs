using Domain.Models.Products;

namespace Application.Storages
{
    public partial interface IStorage
    {
        ValueTask<Product> InsertProductAsync(Product product);
        ValueTask<Product> SelectProductByIdAsync(Guid productId);
        IQueryable<Product> SelectAllProducts();
    }
}
