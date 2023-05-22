using BookShelfApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShelfApp.Repositories
{
    public class SqlRepository
    {
        private readonly DbSet<Book> _dbSet;
        private readonly DbContext _dbContext;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Book>();
        }

        public Book? GetById(int id) 
        {
            return _dbSet.Find(id);
        }

        public void Add (Book item)
        {
            _dbSet.Add(item);
        }

        public void Remove(Book item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        
    }
}
