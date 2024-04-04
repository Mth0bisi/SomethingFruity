using Microsoft.EntityFrameworkCore;
using SomethingFruity.Data;
using SomethingFruity.Data.Repository;
using SomethingFruity.Models;

namespace SomethingFruity.Data.Repository.Categories
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
