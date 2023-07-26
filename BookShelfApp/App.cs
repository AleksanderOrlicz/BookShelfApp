using BookShelfApp.Data;
using BookShelfApp.Entities;
using BookShelfApp.Repositories;

namespace BookShelfApp
{
    public class App : IApp
    {
        private readonly IRepository<BoardGame> _boardGameRepository;

        public App(IRepository<BoardGame> boardGameRepository)
        {            
            _boardGameRepository = boardGameRepository;            
        }
        public void Run()
        {
            Console.WriteLine("I'm here in run method!");

            var boardGames = new[]
            {
                new BoardGame {Title = "Beez"},
                new BoardGame {Title = "Robinson Crusoe"},
                new BoardGame {Title = "Rada Pięciu" }
            };

            foreach (var boardGame in boardGames)
            {
                _boardGameRepository.Add(boardGame);
            }
            _boardGameRepository.Save();

            Console.WriteLine();
            Console.WriteLine("========================================");

            var items = _boardGameRepository.GetAll();
            items = items.OrderBy(item => item.Id);
            foreach (var item in items)
            {
                Console.WriteLine(item);

            }

            Console.WriteLine("========================================");
            Console.WriteLine();
        }
    }
}
