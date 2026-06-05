using BookstoreApp.Data;
using BookstoreApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

// ============================================================
// Bookstore Management Application - ADO.NET Demo
// Demonstrates all 5 User Stories from the assignment
// ============================================================

// Update this connection string to match your SQL Server instance
string connectionString = "Server=.;Database=BookstoreDB;Integrated Security=True;TrustServerCertificate=True;";

var repo = new BookRepository(connectionString);

bool running = true;
while (running)
{
    Console.WriteLine("\n========================================");
    Console.WriteLine("    BOOKSTORE MANAGEMENT - ADO.NET");
    Console.WriteLine("========================================");
    Console.WriteLine("[1]  List all books          (SqlDataReader)");
    Console.WriteLine("[2]  Add a book              (Parameterized SqlCommand)");
    Console.WriteLine("[3]  Update a book           (Parameterized SqlCommand)");
    Console.WriteLine("[4]  Delete a book           (Parameterized SqlCommand)");
    Console.WriteLine("[5]  SQL Injection Demo      (User Story 2)");
    Console.WriteLine("[6]  Add via Stored Proc     (User Story 3)");
    Console.WriteLine("[7]  Update via Stored Proc  (User Story 3)");
    Console.WriteLine("[8]  Delete via Stored Proc  (User Story 3)");
    Console.WriteLine("[9]  View as DataSet         (User Story 4)");
    Console.WriteLine("[10] Update via DataSet      (User Story 4)");
    Console.WriteLine("[11] Add via DataSet         (User Story 4)");
    Console.WriteLine("[0]  Exit");
    Console.Write("\nChoice: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            //  User Story 1 + 5: Read with SqlDataReader 
            case "1":
                Console.WriteLine(" All Books (via SqlDataReader) ");
                var books = repo.GetAllBooksWithReader();
                if (books.Count == 0)
                    Console.WriteLine("No books found.");
                else
                    books.ForEach(b => Console.WriteLine(b));
                break;

            //  User Story 1 + 2: Add with parameterized query 
            case "2":
                var newBook = PromptBook();
                int newId = repo.AddBook(newBook);
                Console.WriteLine($"Book added successfully. New BookId: {newId}");
                break;

            //  User Story 1 + 2: Update with parameterized query 
            case "3":
                Console.Write("Enter BookId to update: ");
                int updateId = int.Parse(Console.ReadLine()!);
                var updatedBook = PromptBook();
                updatedBook.BookId = updateId;
                bool updated = repo.UpdateBook(updatedBook);
                Console.WriteLine(updated ? "Book updated successfully." : "Book not found.");
                break;

            //  User Story 1 + 2: Delete with parameterized query
            case "4":
                Console.Write("Enter BookId to delete: ");
                int deleteId = int.Parse(Console.ReadLine()!);
                bool deleted = repo.DeleteBook(deleteId);
                Console.WriteLine(deleted ? "Book deleted successfully." : "Book not found.");
                break;

            // ---- User Story 2: SQL Injection demonstration ----
            case "5":
                Console.Write("Enter a title to search (try: ' OR '1'='1): ");
                string? userInput = Console.ReadLine() ?? "";
                repo.DemonstrateSqlInjectionPrevention(userInput);
                break;

            //  User Story 3: Add via stored procedure
            case "6":
                var spBook = PromptBook();
                int spId = repo.AddBookViaStoredProcedure(spBook);
                Console.WriteLine($"Book added via stored procedure. New BookId: {spId}");
                break;

            //  User Story 3: Update via stored procedure
            case "7":
                Console.Write("Enter BookId to update via SP: ");
                int spUpdateId = int.Parse(Console.ReadLine()!);
                var spUpdatedBook = PromptBook();
                spUpdatedBook.BookId = spUpdateId;
                repo.UpdateBookViaStoredProcedure(spUpdatedBook);
                Console.WriteLine("Book updated via stored procedure.");
                break;

            // User Story 3: Delete via stored procedure 
            case "8":
                Console.Write("Enter BookId to delete via SP: ");
                int spDeleteId = int.Parse(Console.ReadLine()!);
                repo.DeleteBookViaStoredProcedure(spDeleteId);
                Console.WriteLine("Book deleted via stored procedure.");
                break;

            //  User Story 4: View DataSet / DataTable
            case "9":
                Console.WriteLine("--- Books loaded into DataSet (disconnected) ---");
                DataSet ds = repo.GetBooksAsDataSet();
                DataTable table = ds.Tables["Books"]!;
                Console.WriteLine($"DataTable has {table.Rows.Count} rows and {table.Columns.Count} columns.");
                Console.WriteLine($"Columns: {string.Join(", ", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                    Console.WriteLine($"  [{row["BookId"]}] {row["Title"]} | ₹{row["Price"]} | Stock: {row["Stock"]}");
                break;

            //  User Story 4: Update price/stock via DataSet
            case "10":
                Console.Write("Enter BookId to update via DataSet: ");
                int dsBookId = int.Parse(Console.ReadLine()!);
                Console.Write("New Price: ");
                decimal dsPrice = decimal.Parse(Console.ReadLine()!);
                Console.Write("New Stock: ");
                int dsStock = int.Parse(Console.ReadLine()!);
                repo.UpdateBooksViaDataSet(dsBookId, dsPrice, dsStock);
                break;

            // User Story 4: Add via DataSet 
            case "11":
                var dsBook = PromptBook();
                repo.AddBookViaDataSet(dsBook);
                break;

            case "0":
                running = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    catch (SqlException ex)
    {
        Console.WriteLine($"[SQL ERROR] {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"[VALIDATION ERROR] {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] {ex.Message}");
    }
}

//Helper: prompt user for book details 
static Book PromptBook()
{
    Console.Write("Title:  "); string title = Console.ReadLine()!;
    Console.Write("Author: "); string author = Console.ReadLine()!;
    Console.Write("Genre:  "); string genre = Console.ReadLine()!;
    Console.Write("Price:  "); decimal price = decimal.Parse(Console.ReadLine()!);
    Console.Write("Stock:  "); int stock = int.Parse(Console.ReadLine()!);

    return new Book
    {
        Title = title,
        Author = author,
        Genre = genre,
        Price = price,
        Stock = stock
    };
}