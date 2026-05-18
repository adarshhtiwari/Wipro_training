using Microsoft.EntityFrameworkCore;
using StudentRepositoryDemo.Models;

namespace StudentRepositoryDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {

        }

        // THIS IS REQUIRED
        public DbSet<Student> Students { get; set; }
    }
}