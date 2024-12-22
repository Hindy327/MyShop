using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposetories
{
    public class ProductReposetory : IProductReposetory
    {
        _327725412WebApiContext ConectDb;
        public ProductReposetory(_327725412WebApiContext _327725412WebApiContext)
        {
            ConectDb = _327725412WebApiContext;
        }
        public async Task addProduct(Product product)
        {
            await ConectDb.Products.AddAsync(product);
            await ConectDb.SaveChangesAsync();
        }
        public async Task<List<Product>> GetProducts(int position,int skip,string? desc,int? minPrice, int? maxPrice, int?[] categoryIds) {
            var qury = ConectDb.Products.Where(product =>
            (desc == null ? (true) : (product.Description.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
            Console.WriteLine(qury.ToQueryString());
            List<Product> products = await qury.ToListAsync();
        
            return products;

        }
        public async Task<Product> GetProductById(int id)
        {
            return await ConectDb.Products.FirstOrDefaultAsync(product => product.Id == id);
        }
        public async Task updateProduct(int id, Product Details)
        {
            Details.Id = id;
            ConectDb.Update(Details);
            await ConectDb.SaveChangesAsync();
        }
    }
}
