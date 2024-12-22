using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Reposetories;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductReposetory productReposetory;
        public ProductService(IProductReposetory iproductReposetory)
        {
            productReposetory = iproductReposetory;
        }
        public async Task CreateProduct(Product product)
        {
            await productReposetory.addProduct(product);
        }
        public async Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await productReposetory.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds);
        }
        public async Task<Product> GetProductById(int id)
        {
            return await productReposetory.GetProductById(id);
        }
        public async Task UpdateProduct(int id, Product product)
        {
            await productReposetory.updateProduct(id, product);
        }

    }
}
