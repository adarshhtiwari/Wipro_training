using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // Constructor for runtime (Program.cs passes options)
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        // Parameterless constructor for EF Core Tools at design time
        public LibraryDbContext() { }

        // Fallback connection string for Add-Migration / Update-Database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Integrated Security=True;TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Author → Many Books
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            // Many Books ↔ Many Genres
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    j => j.HasOne<Genre>().WithMany().HasForeignKey("GenreID"),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookID")
                );

            // Book column config
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Price).HasColumnType("decimal(10,2)");
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            });

            // Genre config
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.GenreID);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(50);
            });

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorID = 1, Name = "Robert C. Martin", Bio = "Author of Clean Code." },
                new Author { AuthorID = 2, Name = "Paulo Coelho", Bio = "Author of The Alchemist." },
                new Author { AuthorID = 3, Name = "James Clear", Bio = "Author of Atomic Habits." }
            );

            // Seed Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, Name = "Technology" },
                new Genre { GenreID = 2, Name = "Fiction" },
                new Genre { GenreID = 3, Name = "Self-Help" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookID = 1, Title = "Clean Code", AuthorID = 1, Price = 549, Stock = 10 },
                new Book { BookID = 2, Title = "The Alchemist", AuthorID = 2, Price = 299, Stock = 25 },
                new Book { BookID = 3, Title = "Atomic Habits", AuthorID = 3, Price = 349, Stock = 20 }
            );

            // Seed BookGenre join table
            modelBuilder.Entity("BookGenre").HasData(
                new { BookID = 1, GenreID = 1 },
                new { BookID = 2, GenreID = 2 },
                new { BookID = 3, GenreID = 3 }
            );
        }
    }
}