
using Microsoft.EntityFrameworkCore;

namespace moviesGestion.repositories
{
    public class Trepository<T> : ITRepository<T> where T : class
    {
        private readonly MovieDbContext _context;

        public Trepository(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();




    }
}
