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
            int a=2;
            int b = 0;
            int e = a / b;
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

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
