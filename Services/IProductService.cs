using Entities;

namespace Services
{
    public interface IProductService
    {
        Task CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task UpdateProduct(int id, Product product);
    }
}