using Microsoft.Extensions.Configuration;
using BookShelfApp.Data;
using BookShelfApp.Entities;
using BookShelfApp.Repositories;
using BookShelfApp.Repositories.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BookShelfApp
{

    internal class Program
    {
        private const string boardGamesFile = "boardGames.txt";
        private const string auditFilePath = "auditFile.txt";
         //public const string dbString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MyDatabase; User Id = MyUser; Password = MyPassword1234";



        private static void Main(string[] args)
        {
            bool CloseApp = false;

            //var contextOptions = new DbContextOptionsBuilder<BookShelfAppDbContextDB>()
            //    .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MyDatabase; User Id = MyUser; Password = MyPassword1234")
            //    .Options;
            var boardGameRepository = new SqlRepository<BoardGame>(new BookShelfAppDbContext());
            boardGameRepository.ItemAdded += BoardGameRepository_ItemAdded;
            boardGameRepository.ItemRemoved += BoardGameRepository_ItemRemoved;

            var savedBoardGames = ReadItemsFromFile(boardGamesFile);
            //foreach (var boardGame in savedBoardGames)
            //{                
            //    boardGameRepository.Read(boardGame);
            //    boardGameRepository.Save();
            //}

            ReadToRepository(boardGameRepository, savedBoardGames);
            //AddRPGGames(boardGameRepository);
            //AddMoreBoardGames(boardGameRepository);


            StartScreen();

            while (!CloseApp)
            {
                MainMenu();
                var userAction = Console.ReadLine();

                switch (userAction.ToUpper())
                {
                    case "1":
                        WriteAllToConsole(boardGameRepository);
                        break;
                    case "2":
                        ChooseAddedElement(boardGameRepository);
                        break;
                    case "3":
                        RemoveElementById(boardGameRepository);
                        break;
                    case "4":
                    case "Q":
                        SaveAllToFile(boardGameRepository);
                        CloseApp = true;
                        break;
                    default:
                        Console.WriteLine("Wrong character try again!");
                        continue;
                }
            }




            //var bookRepository = new SqlRepository<Book>(new BookShelfAppDbContext());

            //WriteAllToConsole(boardGameRepository);

            //AddMoreBooks(bookRepository);
            //AddRPGGames(boardGameRepository);
            //AddMoreBoardGames(boardGameRepository);


            //RemoveBoardGame(boardGameRepository, 2);

            //AddBoardGame(boardGameRepository, "Beez");

            //WriteAllToConsole(bookRepository);
            //WriteAllToConsole(boardGameRepository);
        }

        

        /// <summary>
        /// UI
        /// </summary>
        /// <param name="boardGameRepository"></param>
        private static void ChooseAddedElement(IWriteRepository<BoardGame> boardGameRepository)
        {
            Console.WriteLine("Wybierz jaki element chcesz dodać:");
            Console.WriteLine("1 - Gra planszowa");
            Console.WriteLine("2 - Gra RPG");
            Console.WriteLine("Dowolny inny przycisk by anulować.");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                case "2":
                    AddElement(boardGameRepository, userInput);
                    break;
                default:
                    break;
            }
            
        }

        private static void ClearConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;            
        }

        private static void StartScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("====================================");
            Console.WriteLine("Witaj w aplikacji zbierającej dane Twojej półki na książki.");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void MainMenu()
        {
            Console.WriteLine("Wybierz akcję do wykonania:");
            Console.WriteLine("1 - Wypisz wszystkie elementy");
            Console.WriteLine("2 - Dodaj element");
            Console.WriteLine("3 - Usuń element");
            Console.WriteLine("4/Q - Zapisz i wyjdź");
        }

        //for EventHandler
        public static void BoardGameRepository_ItemAdded(object? sender, BoardGame item)
        {
            //Console.WriteLine($"Board Game Added => {item.Title} from {sender?.GetType().Name}");
            SaveToAuditFile($"{item.GetType().Name} Added => {item.Title}");      
            //SaveItemToFile(item);
        }
        private static void BoardGameRepository_ItemRemoved(object? sender, BoardGame item)
        {
            //Console.WriteLine($"{item.GetType().Name} Removed => {item.Title}");
            SaveToAuditFile($"{item.GetType().Name} Removed => {item.Title}");
        }

        ////for delegate
        //static void BoardGameAdded(BoardGame item)
        //{
        //    Console.WriteLine($"{item.Title} added");
        //}
        //private static void BoardGameRemoved(BoardGame item)
        //{
        //    Console.WriteLine($"{item.Title} removed");
        //}
        public static void SaveItemToFile<T>(T item)
            where T : class
        {
            string jsonString = JsonSerializer.Serialize(item);
            try
            {                
                using (var writer = File.AppendText(boardGamesFile))
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error during writing into file" + ex.Message);
            }
        }

        private static void SaveAllToFile(IReadRepository<BoardGame> repository)
            
        {
            File.Delete(boardGamesFile);
            var items = ListAllById(repository);
            foreach (var item in items)
            {
                SaveItemToFile(item);
            }
        }

        public static List<BoardGame> ReadItemsFromFile(string filePath)            
        {
            var readedObjects = new List<BoardGame>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)

                    {
                    var item = JsonSerializer.Deserialize<BoardGame>(line);                    
                    readedObjects.Add(item);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error during writing into file" + ex.Message);
            }
            return readedObjects;
        }

        public static void ReadToRepository(IWriteRepository<BoardGame> repository, List<BoardGame> items)            
        {
            foreach (var item in items)
            {
                repository.Read(item);
                repository.Save();
            }
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

        private static void AddElement(IWriteRepository<BoardGame> boardGameRepository, string type)
        {
            Console.Write("Podaj Tytuł nowego elementu: ");
            var userTitle = Console.ReadLine();

            switch (type)
            {
                case "1":
                    AddBoardGame(boardGameRepository, userTitle);
                    break;
                case "2":
                    AddRPGGame(boardGameRepository, userTitle);
                    break;
                default:
                    Console.WriteLine("Nie można zapisać tego typu do półki");
                    break;
            }
        }
        private static void AddBoardGame(IWriteRepository<BoardGame> boardGameRepository, string title)
        {
            boardGameRepository.Add(new BoardGame { Title = title });
            boardGameRepository.Save();
        }
        //adding single entities
        private static void AddRPGGame(IWriteRepository<BoardGame> rpgRepository, string title)
        {
            rpgRepository.Add(new RolePlayGame { Title = title });
            rpgRepository.Save();
        }

        private static void AddBook(IWriteRepository<Book> bookRepository, string title)
        {
            bookRepository.Add(new Book { Title = title });
            bookRepository.Save();
        }

        private static  void RemoveElementById<T>(IWriteRepository<T> writeRepository)
            where T : EntityBase, new()
        {
            Console.Write("Podaj Id Elementu do usunięcia: ");
            string userId = Console.ReadLine();

            if(int.TryParse(userId, out int Id))
            {
                writeRepository.RemoveById(Id);
                writeRepository.Save();
            }
            else
            {
                Console.WriteLine("Id musi być cyfrą!!! Sprobój ponownie");
            }    

            
        }

        private static void RemoveBoardGame(IWriteRepository<BoardGame> boardGameRepository, int Id)
        {
            boardGameRepository.RemoveById(Id);
            boardGameRepository.Save();
        }
               

        //TO WORK WITH!!!
        //private static void AddGenericElement<T>(IWriteRepository<T> repository,  string title)
        //    where T : EntityBase, new()
        //{
        //    T element = new T();
        //    element.Title = title;
        //    repository.Add(element);
        //    repository.Save();
        //}
        //private static void RemoveGenericElement<T>(IWriteRepository<T> repository, int Id)
        //    where T : class, IEntity
        //{
        //    repository.RemoveById(Id);
        //    repository.Save();
        //}

        static void AddMoreBooks(IRepository<Book> bookRepository)
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
            ClearConsole();
            Console.WriteLine();
            Console.WriteLine("========================================");

            var items = repository.GetAll();
            items = items.OrderBy(item => item.Id);
            foreach (var item in items)
            {
                Console.WriteLine(item);
                
            }

            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        //do zapisu do pliku?

        static List<BoardGame> ListAllById(IReadRepository<BoardGame> repository)
        {
            var listedAllById = new List<BoardGame>();
            var items = repository.GetAll();
            items = items.OrderBy(item => item.Id);
            foreach(var item in items)
            {
                listedAllById.Add(item);
            }
            return listedAllById;
        }


    }
}