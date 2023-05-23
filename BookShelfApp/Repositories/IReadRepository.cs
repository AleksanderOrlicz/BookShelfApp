using BookShelfApp.Entities;

namespace BookShelfApp.Repositories
{
    public interface IReadRepository<out T>
        where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
