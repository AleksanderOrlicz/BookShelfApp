//using BookShelfApp.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;


//namespace BookShelfApp.Data
//{
//    internal class BookShelfAppDbContextDB : DbContext
//    {
//        public BookShelfAppDbContextDB(DbContextOptions<BookShelfAppDbContextDB> options) 
//            : base(options) { }
//        public DbSet<Book> Books { get; set; }

//        public DbSet<BoardGame> BoardGames { get; set; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            //dodaj konfiguracje dbContext
//            services.AddDbContext<BookShelfAppDbContextDB> (options =>
//                options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MyDatabase; User Id = MyUser; Password = MyPassword1234"));
//        }

//        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        //{
//        //    base.OnConfiguring(optionsBuilder);
//        //    optionsBuilder.UseInMemoryDatabase("StorageAppDb");
//        //}
//    }
//}
