using BookShelfApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookShelfApp.Data
{
    public class BookShelfAppDbContext : DbContext
    {
        public BookShelfAppDbContext(DbContextOptions<BookShelfAppDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BoardGame> BoardGames => Set<BoardGame>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
