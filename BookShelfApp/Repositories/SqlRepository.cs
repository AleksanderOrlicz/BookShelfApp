using BookShelfApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShelfApp.Repositories
{ 
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        //private readonly Action<T>? _itemAddedCallback;
        //private readonly Action<T>? _itemRemovedCallback;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            //_itemAddedCallback = itemAddedCallback;
            //_itemRemovedCallback = itemRemovedCallback;
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public T GetById(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Element o podanym Id nie istnieje!");
            }
             return entity;
            
            
        }

        //used to read from file
        public void Read(T item)
        {
            _dbSet.Add(item);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            //_itemAddedCallback?.Invoke(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            //_itemRemovedCallback?.Invoke(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void RemoveById(int Id)
        {
            try
            {
                 var toRemove = GetById(Id);
                Remove(toRemove);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }                        
        }
    }
}
