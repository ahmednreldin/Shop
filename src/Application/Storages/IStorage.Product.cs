using Domain.Models.Products;

namespace Application.Storages
{
    public partial interface IStorage
    {
        ValueTask<Product> InsertProductAsync(Product product);
        ValueTask<Product> SelectProductById(Guid productId);
        IQueryable<Product> SelectAllProducts();
    }
}
