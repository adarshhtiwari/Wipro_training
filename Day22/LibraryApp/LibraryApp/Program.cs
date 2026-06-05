using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Repositories;

//Configure EF Core (Code First) 
var options = new DbContextOptionsBuilder<LibraryDbContext>()
    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Integrated Security=True;TrustServerCertificate=True;")
    .Options;

using var context = new LibraryDbContext(options);

// Apply any pending migrations automatically on startup
await context.Database.MigrateAsync();

var bookRepo = new BookRepository(context);
var authorRepo = new AuthorRepository(context);
var genreRepo = new GenreRepository(context);

bool running = true;
while (running)
{
    Console.WriteLine("   LIBRARY MANAGEMENT SYSTEM - EF Core");
    Console.WriteLine("[1]  List all books (simple)");
    Console.WriteLine("[2]  List all books with Author + Genres");
    Console.WriteLine("[3]  Add a book");
    Console.WriteLine("[4]  Update a book");
    Console.WriteLine("[5]  Delete a book");
    Console.WriteLine("[6]  Search books by genre");
    Console.WriteLine("[7]  Search books by author name");
    Console.WriteLine("[8]  List all authors");
    Console.WriteLine("[9]  Add an author");
    Console.WriteLine("[10] Delete an author");
    Console.WriteLine("[11] List all genres");
    Console.WriteLine("[12] Add a genre");
    Console.WriteLine("[13] Assign genre to a book");
    Console.WriteLine("[0]  Exit");
    Console.Write("\nChoice: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            // List all books (simple, no joins)
            case "1":
                var books = await bookRepo.GetAllAsync();
                if (!books.Any())
                    Console.WriteLine("No books found.");
                else
                    foreach (var b in books)
                        Console.WriteLine($"  [{b.BookID}] {b.Title} | AuthorID: {b.AuthorID} | ₹{b.Price} | Stock: {b.Stock}");
                break;

            // List all books with Author + Genres (eager loading)
            case "2":
                var detailedBooks = await bookRepo.GetAllWithDetailsAsync();
                Console.WriteLine("All Books with Author & Genres");
                if (!detailedBooks.Any())
                    Console.WriteLine("No books found.");
                else
                    foreach (var b in detailedBooks)
                    {
                        string genres = b.Genres.Any()
                            ? string.Join(", ", b.Genres.Select(g => g.Name))
                            : "None";
                        Console.WriteLine($"  [{b.BookID}] \"{b.Title}\" by {b.Author.Name} | ₹{b.Price} | Stock: {b.Stock} | Genres: {genres}");
                    }
                break;

            //Add a book
            case "3":
                Console.Write("Title:    ");
                string title = Console.ReadLine()!;
                Console.Write("AuthorID: ");
                int authorId = int.Parse(Console.ReadLine()!);
                Console.Write("Price:    ");
                decimal price = decimal.Parse(Console.ReadLine()!);
                Console.Write("Stock:    ");
                int stock = int.Parse(Console.ReadLine()!);

                var newBook = new Book
                {
                    Title = title,
                    AuthorID = authorId,
                    Price = price,
                    Stock = stock
                };
                var added = await bookRepo.AddAsync(newBook);
                Console.WriteLine($"Book added successfully. New BookID: {added.BookID}");
                break;

            //Update a book
            case "4":
                Console.Write("BookID to update: ");
                int updId = int.Parse(Console.ReadLine()!);
                Console.Write("New Title:        ");
                string nTitle = Console.ReadLine()!;
                Console.Write("New AuthorID:     ");
                int nAuthorId = int.Parse(Console.ReadLine()!);
                Console.Write("New Price:        ");
                decimal nPrice = decimal.Parse(Console.ReadLine()!);
                Console.Write("New Stock:        ");
                int nStock = int.Parse(Console.ReadLine()!);

                bool updated = await bookRepo.UpdateAsync(new Book
                {
                    BookID = updId,
                    Title = nTitle,
                    AuthorID = nAuthorId,
                    Price = nPrice,
                    Stock = nStock
                });
                Console.WriteLine(updated ? "Book updated successfully." : "Book not found.");
                break;

            //Delete a book
            case "5":
                Console.Write("BookID to delete: ");
                int deleteId = int.Parse(Console.ReadLine()!);
                bool deleted = await bookRepo.DeleteAsync(deleteId);
                Console.WriteLine(deleted ? "Book deleted successfully." : "Book not found.");
                break;

            //Search books by genre 
            case "6":
                Console.Write("Enter genre name: ");
                string genreName = Console.ReadLine()!;
                var byGenre = await bookRepo.GetByGenreAsync(genreName);
                if (!byGenre.Any())
                    Console.WriteLine("No books found for that genre.");
                else
                    foreach (var b in byGenre)
                        Console.WriteLine($"  [{b.BookID}] {b.Title} by {b.Author.Name}");
                break;

            //Search books by author name 
            case "7":
                Console.Write("Enter author name: ");
                string authorName = Console.ReadLine()!;
                var byAuthor = await bookRepo.GetByAuthorNameAsync(authorName);
                if (!byAuthor.Any())
                    Console.WriteLine("No books found for that author.");
                else
                    foreach (var b in byAuthor)
                        Console.WriteLine($"  [{b.BookID}] {b.Title} | ₹{b.Price} | Stock: {b.Stock}");
                break;

            //List all authors 
            case "8":
                var authors = await authorRepo.GetAllAsync();
                if (!authors.Any())
                    Console.WriteLine("No authors found.");
                else
                    foreach (var a in authors)
                        Console.WriteLine($"  [{a.AuthorID}] {a.Name} | Books: {a.Books.Count} | Bio: {a.Bio}");
                break;

            //Add an author
            case "9":
                Console.Write("Author Name: ");
                string aName = Console.ReadLine()!;
                Console.Write("Bio:         ");
                string aBio = Console.ReadLine()!;

                var newAuthor = await authorRepo.AddAsync(new Author
                {
                    Name = aName,
                    Bio = aBio
                });
                Console.WriteLine($"Author added successfully. New AuthorID: {newAuthor.AuthorID}");
                break;

            //Delete an author
            case "10":
                Console.Write("AuthorID to delete: ");
                int aDeleteId = int.Parse(Console.ReadLine()!);
                bool aDeleted = await authorRepo.DeleteAsync(aDeleteId);
                Console.WriteLine(aDeleted ? "Author deleted successfully." : "Author not found.");
                break;

            //List all genres
            case "11":
                {
                    var genres = await genreRepo.GetAllAsync();
                    if (!genres.Any())
                        Console.WriteLine("No genres found.");
                    else
                        foreach (var g in genres)
                            Console.WriteLine($"  [{g.GenreID}] {g.Name} | Books: {g.Books.Count}");
                    break;
                }

            //Add a genre
            case "12":
                Console.Write("Genre Name: ");
                string gName = Console.ReadLine()!;
                var newGenre = await genreRepo.AddAsync(new Genre { Name = gName });
                Console.WriteLine($"Genre added successfully. New GenreID: {newGenre.GenreID}");
                break;

            // Assign a genre to a book
            case "13":
                Console.Write("BookID:  ");
                int bId = int.Parse(Console.ReadLine()!);
                Console.Write("GenreID: ");
                int gId = int.Parse(Console.ReadLine()!);
                await genreRepo.AssignGenreToBookAsync(bId, gId);
                Console.WriteLine($"Genre {gId} assigned to Book {bId} successfully.");
                break;

            case "0":
                running = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid choice. Please enter a number from the menu.");
                break;
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine($"{ex.InnerException?.Message ?? ex.Message}");
    }
    catch (FormatException)
    {
        Console.WriteLine("Please enter a valid number.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
}