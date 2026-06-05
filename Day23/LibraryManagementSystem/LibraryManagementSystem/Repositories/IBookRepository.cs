using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithDetailsAsync();

        Task<IEnumerable<Book>> SearchBooksAsync(string searchText);
    }
}