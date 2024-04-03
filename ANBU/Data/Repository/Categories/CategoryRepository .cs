using Microsoft.EntityFrameworkCore;
using ANBU.Data;
using ANBU.Data.Repository;
using ANBU.Models;

namespace ANBU.Data.Repository.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);
    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(SomethingFruityDbContext context) : base(context)
        {
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _entities.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}
