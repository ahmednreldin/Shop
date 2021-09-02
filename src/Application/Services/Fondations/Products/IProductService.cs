using Domain.Models.Products;

namespace Application.Services.Fondations.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
        ValueTask<Product> RetrieveProductByIdAsync(Guid productId);
        IQueryable<Product> RetrieveAllProducts();
    }
}
