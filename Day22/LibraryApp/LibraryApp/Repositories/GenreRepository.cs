using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public class GenreRepository
    {
        private readonly LibraryDbContext _context;

        public GenreRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.GenreID == id);
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .AsNoTracking()
                .Include(g => g.Books)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Genre genre)
        {
            var existing = await _context.Genres.FindAsync(genre.GenreID);
            if (existing == null) return false;

            existing.Name = genre.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AssignGenreToBookAsync(int bookId, int genreId)
        {
            var book = await _context.Books
                           .Include(b => b.Genres)
                           .FirstOrDefaultAsync(b => b.BookID == bookId)
                       ?? throw new InvalidOperationException($"Book {bookId} not found.");

            var genre = await _context.Genres.FindAsync(genreId)
                        ?? throw new InvalidOperationException($"Genre {genreId} not found.");

            if (!book.Genres.Contains(genre))
            {
                book.Genres.Add(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}