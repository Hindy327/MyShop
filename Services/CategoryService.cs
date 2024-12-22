using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reposetories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryReposetory categoryReposetory;
        public CategoryService(ICategoryReposetory categoryReposetory)
        {
            this.categoryReposetory = categoryReposetory;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await categoryReposetory.GetCategories();
        }

    }
}
