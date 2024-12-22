using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using AutoMapper;
using System.Collections.Generic;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        IMapper mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // GET: api/<CategoryController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            IEnumerable<Category> categories=await categoryService.GetCategories();
            IEnumerable<CategoryDTO> CategoryDTO = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

            return CategoryDTO;
        }   

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
