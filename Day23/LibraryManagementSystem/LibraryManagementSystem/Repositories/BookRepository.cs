using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository :
        GenericRepository<Book>,
        IBookRepository
    {
        public BookRepository(LibraryDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Book>>
            GetBooksWithDetailsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>>
            SearchBooksAsync(string searchText)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.Title.Contains(searchText))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }
    }
}