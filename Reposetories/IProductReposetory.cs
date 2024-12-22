using Entities;

namespace Reposetories
{
    public interface IProductReposetory
    {
        Task addProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task updateProduct(int id, Product Details);
    }
}