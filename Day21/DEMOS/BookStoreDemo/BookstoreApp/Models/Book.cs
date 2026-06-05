namespace BookstoreApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"[{BookId}] {Title} by {Author} | {Genre} | ₹{Price:F2} | Stock: {Stock}";
        }
    }
}