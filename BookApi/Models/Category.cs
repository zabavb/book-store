namespace BookApi.Models
{
    internal class Category
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = null!;

        public List<Book> Books { get; set; } = new();
    }

}
