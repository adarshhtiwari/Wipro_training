using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class GenreRepository :
        GenericRepository<Genre>,
        IGenreRepository
    {
        public GenreRepository(
            LibraryDbContext context)
            : base(context)
        {
        }
    }
}