using BookShelfApp.Entities;

namespace BookShelfApp.Repositories
{
    public interface IWriteRepository<in T>
        where T : class, IEntity
    {
        void Add(T item);
        void Remove(T item);
        void RemoveById(int Id);
        void Save();
    }
}
