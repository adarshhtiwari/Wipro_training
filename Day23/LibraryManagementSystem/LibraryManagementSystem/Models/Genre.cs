namespace LibraryManagementSystem.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }
    }
}