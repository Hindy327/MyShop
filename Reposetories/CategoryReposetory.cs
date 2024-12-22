using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposetories
{
    public class CategoryReposetory : ICategoryReposetory
    {
        _327725412WebApiContext ConectDb;
        public CategoryReposetory(_327725412WebApiContext _327725412WebApiContext)
        {
            ConectDb = _327725412WebApiContext;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await ConectDb.Categories.ToListAsync();
        }
    }
}
