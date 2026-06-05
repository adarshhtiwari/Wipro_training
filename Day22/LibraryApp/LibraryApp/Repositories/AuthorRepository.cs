using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
    public class AuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorID == id);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var existing = await _context.Authors.FindAsync(author.AuthorID);
            if (existing == null) return false;

            existing.Name = author.Name;
            existing.Bio = author.Bio;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorID == id);

            if (author == null) return false;

            if (author.Books.Any())
                throw new InvalidOperationException(
                    $"Cannot delete '{author.Name}' — they have {author.Books.Count} book(s) assigned.");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}