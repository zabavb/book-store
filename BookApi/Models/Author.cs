namespace BookApi.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Biography { get; set; }

        internal ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
