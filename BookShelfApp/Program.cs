using BookShelfApp.Data;
using BookShelfApp.Entities;
using BookShelfApp.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var sqlRepository = new SqlRepository<BoardGame>(new BookShelfAppDbContext());
        
        AddBoardGames(sqlRepository);
        AddRPGGames(sqlRepository);
        WriteAllToConsole(sqlRepository);
        
       
    }

    static void AddBoardGames(IRepository<BoardGame> boardGameRepository)
    {
        boardGameRepository.Add(new BoardGame { Title = "Mechanik" });
        boardGameRepository.Add(new BoardGame { Title = "Homo deus" });
        boardGameRepository.Add(new BoardGame { Title = "Diuna" });
        boardGameRepository.Save();
    }

    static void AddRPGGames(IWriteRepository<RolePlayGame> rpgRepository)
    {
        rpgRepository.Add(new RolePlayGame { Title = "Dark Heresy 2ed." });
        rpgRepository.Add(new RolePlayGame { Title = "Diuna: Imperium" });
        rpgRepository.Save();
    }

    static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }


}