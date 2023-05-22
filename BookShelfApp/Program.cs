using BookShelfApp.Data;
using BookShelfApp.Entities;
using BookShelfApp.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var sqlRepository = new SqlRepository<Book>(new BookShelfAppDbContext());
        sqlRepository.Add(new Book { Title = "Mechanik" });
        sqlRepository.Add(new Book { Title = "Homo deus" });
        sqlRepository.Add(new Book { Title = "Diuna" });
        sqlRepository.Save();
        var emp = sqlRepository.GetById(1);
        Console.WriteLine(emp.ToString());
    }
}