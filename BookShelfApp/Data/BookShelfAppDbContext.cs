using BookShelfApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShelfApp.Data
{
    internal class BookShelfAppDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BoardGame> BoardGames => Set<BoardGame>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
