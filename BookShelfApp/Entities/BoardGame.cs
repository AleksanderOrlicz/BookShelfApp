namespace BookShelfApp.Entities
{
    public class BoardGame : EntityBase
    {
        public string? Title { get; set; }
        public const string? Category = "Board Game";
        //public string Description { get; set; }
        //public string Author { get; set; }
        
        public override string ToString() => $"Id: {Id}, Title: {Title} ";
    }
}
