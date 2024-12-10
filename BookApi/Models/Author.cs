namespace BookApi.Models
{
    internal class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }


        internal ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
