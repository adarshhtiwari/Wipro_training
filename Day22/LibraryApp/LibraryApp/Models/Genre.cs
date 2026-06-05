namespace LibraryApp.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}