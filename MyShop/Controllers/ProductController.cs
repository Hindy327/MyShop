using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;
using AutoMapper;
using System.Collections.Generic;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService productService;
        IMapper mapper;
        public ProductController(IProductService iproductService, IMapper mapper)
        {
            productService = iproductService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>>  GetProducts([FromQuery]int position, [FromQuery] int skip, [FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            IEnumerable<Product> products= await productService.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds);
            IEnumerable<ProductDTO> productDTO = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return productDTO;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ProductDTO> Get(int id)
        {
            Product product = await productService.GetProductById(id);
        
            ProductDTO productDTO = mapper.Map<Product, ProductDTO>(product);
            return productDTO;
        }
    }
}
