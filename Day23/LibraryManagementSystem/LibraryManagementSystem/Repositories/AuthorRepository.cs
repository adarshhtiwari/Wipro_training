using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class AuthorRepository :
        GenericRepository<Author>,
        IAuthorRepository
    {
        public AuthorRepository(
            LibraryDbContext context)
            : base(context)
        {
        }
    }
}