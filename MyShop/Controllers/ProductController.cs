using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;
using AutoMapper;
using System.Collections.Generic;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public ProductController(IProductService iproductService, IMapper mapper,IDistributedCache distributedCache) 
        {
            productService = iproductService;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProducts([FromQuery] int position, [FromQuery] int skip, [FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            string cacheKey = $"products_{position}_{skip}_{desc}_{minPrice}_{maxPrice}_{string.Join(",", categoryIds ?? new int?[0])}";
            string cachedProducts = await distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(cachedProducts);
            }

            IEnumerable<Product> products = await productService.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds);
            IEnumerable<ProductDTO> productDTO = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(productDTO), cacheOptions);

            return productDTO;
        }

        [HttpGet("{id}")]
        public async Task<ProductDTO> Get(int id)
        {
            string cacheKey = $"product_{id}";
            string cachedProduct = await distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedProduct))
            {
                return JsonSerializer.Deserialize<ProductDTO>(cachedProduct);
            }

            Product product = await productService.GetProductById(id);
            ProductDTO productDTO = mapper.Map<Product, ProductDTO>(product);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(productDTO), cacheOptions);

            return productDTO;
        }
    }
}