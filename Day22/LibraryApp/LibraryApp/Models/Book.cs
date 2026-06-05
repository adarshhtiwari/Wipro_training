using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorID { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        [ForeignKey("AuthorID")]
        public Author Author { get; set; } = null!;

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}