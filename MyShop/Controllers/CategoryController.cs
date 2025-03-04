using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using AutoMapper;
using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        IMapper mapper;
        private readonly IMemoryCache memoryCache;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IMemoryCache memoryCache)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            if (!memoryCache.TryGetValue("categories", out IEnumerable<Category> categories))
            {
                categories = await categoryService.GetCategories();
                memoryCache.Set("categories", categories, TimeSpan.FromMinutes(2));
            }
            IEnumerable<CategoryDTO> categoryDTOList = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
            return categoryDTOList != null ? Ok(categoryDTOList) : BadRequest();
        }
    }
}
