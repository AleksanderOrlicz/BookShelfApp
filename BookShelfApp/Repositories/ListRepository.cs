using BookShelfApp.Entities;

namespace BookShelfApp.Repositories
{
    public class ListRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Save()
        {
            //save is not required in list
        }

        public void RemoveById(int Id)
        {
            var toRemove = GetById(Id);
            Remove(toRemove);
        }

        public void Read(T item)
        {            
            _items.Add(item);
        }
    }
}
