using BookShelfApp.Data;
using BookShelfApp.Entities;
using BookShelfApp.Repositories;
using BookShelfApp.Repositories.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;

namespace BookShelfApp
{
    
    internal class Program
    {
        private const string auditFilePath = "auditFile.txt";

        private static void Main(string[] args)
        {
            


            var bookRepository = new SqlRepository<Book>(new BookShelfAppDbContext());
            var boardGameRepository = new SqlRepository<BoardGame>(new BookShelfAppDbContext(), BoardGameAdded);
            boardGameRepository.ItemAdded += BoardGameRepository_ItemAdded; ;

            AddBooks(bookRepository);
            AddRPGGames(boardGameRepository);
            AddMoreBoardGames(boardGameRepository);



            RemoveBoardGame(boardGameRepository, 2);
            AddBoardGame(boardGameRepository, "Beez");

            WriteAllToConsole(bookRepository);
            WriteAllToConsole(boardGameRepository);


        }

        //for EventHandler
        public static void BoardGameRepository_ItemAdded(object? sender, BoardGame item)
        {
            //Console.WriteLine($"Board Game Added => {item.Title} from {sender?.GetType().Name}");
            SaveToAuditFile($"{item.GetType().Name} Added => {item.Title} ");
        }

        //for delegate
        static void BoardGameAdded(BoardGame item)
        {
            Console.WriteLine($"{item.Title} added");
        }

        public static void SaveToAuditFile(string message)
        {
            try
            {
                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string logline = ($"[{formattedDateTime}] - {message}");
                using (var writer = File.AppendText(auditFilePath))
                {
                    writer.WriteLine(logline);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error during writing into file" + ex.Message);
            }            
        }
        private static void AddBoardGame(IWriteRepository<BoardGame> boardGameRepository, string title)
        {
            boardGameRepository.Add(new BoardGame { Title = title });
            boardGameRepository.Save();
        }

        private static void RemoveBoardGame(IWriteRepository<BoardGame> boardGameRepository, int Id)
        {
            boardGameRepository.RemoveById(Id);
            boardGameRepository.Save();
        }

        static void AddBooks(IRepository<Book> bookRepository)
        {
            var books = new[]
            {
            new Book { Title = "Homo deus" },
            new Book { Title = "Diuna" },
            new Book { Title = "Mechanik" }
        };

            bookRepository.AddBatch(books);
        }




        static void AddMoreBoardGames(IRepository<BoardGame> boardGameRepository)
        {
            var boardGames = new[]
            {
        new BoardGame { Title = "Takenoko" },
        new BoardGame { Title = "Everdell" },
        new BoardGame { Title = "Gloomhaven" }
        };
            boardGameRepository.AddBatch(boardGames);
        }

        static void AddRPGGames(IWriteRepository<BoardGame> rpgRepository)
        {
            rpgRepository.Add(new RolePlayGame { Title = "Dark Heresy 2ed." });
            rpgRepository.Add(new RolePlayGame { Title = "Diuna: Imperium" });
            rpgRepository.Save();
        }

        static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            items = items.OrderBy(item => item.Id);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }


    }
}