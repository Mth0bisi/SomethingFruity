using Microsoft.EntityFrameworkCore;
using ANBU.Data;

namespace ANBU.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task CreateRange(List<T> entity);
        void Update(T entity);
        Task Save();
        void Delete(T entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SomethingFruityDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(SomethingFruityDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public List<T> GetAll() =>
            _entities.ToList();

        public async Task<T> GetById(int id) =>
             await _entities.FindAsync(id);

        public async Task Create(T entity) =>
         await _context.AddAsync(entity);

        public async Task CreateRange(List<T> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task Save() =>
      await _context.SaveChangesAsync();
    }
}
