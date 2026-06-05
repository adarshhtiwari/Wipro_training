using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public class BookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<List<Book>> GetAllWithDetailsAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.BookID);
            if (existing == null) return false;

            existing.Title = book.Title;
            existing.AuthorID = book.AuthorID;
            existing.Price = book.Price;
            existing.Stock = book.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetByGenreAsync(string genreName)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Where(b => b.Genres.Any(g => g.Name == genreName))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<List<Book>> GetByAuthorNameAsync(string authorName)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Where(b => b.Author.Name.Contains(authorName))
                .ToListAsync();
        }
    }
}