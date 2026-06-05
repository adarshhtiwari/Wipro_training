using System.Data;
using Microsoft.Data.SqlClient;
using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    // ============================================================
    // BookRepository: Handles all ADO.NET database operations.
    // Covers:
    //   - User Story 1: CRUD via SqlConnection + SqlCommand
    //   - User Story 2: Parameterized queries (SQL injection prevention)
    //   - User Story 3: Stored procedures
    //   - User Story 4: DataSet + DataTable (disconnected architecture)
    //   - User Story 5: SqlDataReader + SqlDataAdapter
    // ============================================================
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ----------------------------------------------------------
        // USER STORY 1 + 5: Read all books using SqlDataReader
        // SqlDataReader = connected, forward-only, fast read
        // ----------------------------------------------------------
        public List<Book> GetAllBooksWithReader()
        {
            var books = new List<Book>();

            const string sql = "SELECT BookId, Title, Author, Genre, Price, Stock, CreatedAt FROM Books ORDER BY BookId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(MapReaderToBook(reader));
                    }
                }
            }

            return books;
        }

        // ----------------------------------------------------------
        // USER STORY 1 + 2: Add book using parameterized SqlCommand
        // Parameterized query prevents SQL injection
        // ----------------------------------------------------------
        public int AddBook(Book book)
        {
            ValidateBook(book);

            // Parameterized INSERT — user input NEVER concatenated into SQL string
            const string sql = @"
                INSERT INTO Books (Title, Author, Genre, Price, Stock)
                VALUES (@Title, @Author, @Genre, @Price, @Stock);
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Parameters bind values safely — no injection possible
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Stock);

                conn.Open();
                object? result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        // ----------------------------------------------------------
        // USER STORY 1 + 2: Update book using parameterized SqlCommand
        // ----------------------------------------------------------
        public bool UpdateBook(Book book)
        {
            ValidateBook(book);

            const string sql = @"
                UPDATE Books
                SET Title  = @Title,
                    Author = @Author,
                    Genre  = @Genre,
                    Price  = @Price,
                    Stock  = @Stock
                WHERE BookId = @BookId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Stock);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // ----------------------------------------------------------
        // USER STORY 1 + 2: Delete book using parameterized SqlCommand
        // ----------------------------------------------------------
        public bool DeleteBook(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException("BookId must be a positive integer.");

            const string sql = "DELETE FROM Books WHERE BookId = @BookId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // ----------------------------------------------------------
        // USER STORY 2: Demonstrate SQL injection prevention
        // Shows the UNSAFE approach vs the SAFE parameterized approach
        // ----------------------------------------------------------
        public void DemonstrateSqlInjectionPrevention(string userInput)
        {
            Console.WriteLine("\n--- SQL Injection Prevention Demo ---");

            // UNSAFE (DO NOT USE): string concatenation — allows injection
            string unsafeSql = $"SELECT * FROM Books WHERE Title = '{userInput}'";
            Console.WriteLine($"UNSAFE SQL (never do this): {unsafeSql}");
            Console.WriteLine("  ^ If input is: ' OR '1'='1 — this returns ALL rows!");

            // SAFE: parameterized query — input treated as data, never as SQL
            const string safeSql = "SELECT * FROM Books WHERE Title = @Title";
            Console.WriteLine($"SAFE SQL (parameterized):   {safeSql}");
            Console.WriteLine("  ^ @Title is bound as a value; malicious input has no effect.");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(safeSql, conn))
            {
                cmd.Parameters.AddWithValue("@Title", userInput);
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read()) count++;
                Console.WriteLine($"  Safe query returned {count} row(s) for input: \"{userInput}\"");
            }
        }

        // ----------------------------------------------------------
        // USER STORY 3: Add book via stored procedure (with OUTPUT param)
        // ----------------------------------------------------------
        public int AddBookViaStoredProcedure(Book book)
        {
            ValidateBook(book);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_AddBook", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Stock);

                // Output parameter to capture new BookId
                SqlParameter outputParam = new SqlParameter("@NewBookId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputParam.Value;
            }
        }

        // ----------------------------------------------------------
        // USER STORY 3: Update book via stored procedure
        // ----------------------------------------------------------
        public void UpdateBookViaStoredProcedure(Book book)
        {
            ValidateBook(book);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateBook", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Stock", book.Stock);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ----------------------------------------------------------
        // USER STORY 3: Delete book via stored procedure
        // ----------------------------------------------------------
        public void DeleteBookViaStoredProcedure(int bookId)
        {
            if (bookId <= 0)
                throw new ArgumentException("BookId must be a positive integer.");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_DeleteBook", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ----------------------------------------------------------
        // USER STORY 3: Get all books via stored procedure
        // ----------------------------------------------------------
        public List<Book> GetAllBooksViaStoredProcedure()
        {
            var books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAllBooks", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    books.Add(MapReaderToBook(reader));
            }

            return books;
        }

        // ----------------------------------------------------------
        // USER STORY 4 + 5: Load books into DataSet (disconnected)
        // SqlDataAdapter fills DataSet — connection is opened/closed internally
        // ----------------------------------------------------------
        public DataSet GetBooksAsDataSet()
        {
            const string sql = "SELECT BookId, Title, Author, Genre, Price, Stock FROM Books ORDER BY BookId";

            DataSet dataSet = new DataSet("BookstoreData");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                // Auto-generates UPDATE/INSERT/DELETE commands for batch updates
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataSet, "Books");
            }

            return dataSet;
        }

        // ----------------------------------------------------------
        // USER STORY 4: Modify DataTable in-memory and push to DB
        // Demonstrates disconnected architecture with DataSet
        // ----------------------------------------------------------
        public void UpdateBooksViaDataSet(int bookId, decimal newPrice, int newStock)
        {
            const string sql = "SELECT BookId, Title, Author, Genre, Price, Stock FROM Books ORDER BY BookId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Books");

                DataTable table = dataSet.Tables["Books"]!;

                // Find and modify the row in-memory (disconnected)
                DataRow[] rows = table.Select($"BookId = {bookId}");
                if (rows.Length == 0)
                    throw new InvalidOperationException($"Book {bookId} not found in DataTable.");

                rows[0]["Price"] = newPrice;
                rows[0]["Stock"] = newStock;

                // Push all pending changes back to the database in one call
                adapter.Update(dataSet, "Books");

                Console.WriteLine($"  DataSet update committed for BookId {bookId}: Price=₹{newPrice}, Stock={newStock}");
            }
        }

        // ----------------------------------------------------------
        // USER STORY 4: Add a row directly into DataTable, then sync to DB
        // ----------------------------------------------------------
        public void AddBookViaDataSet(Book book)
        {
            const string sql = "SELECT BookId, Title, Author, Genre, Price, Stock FROM Books";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Books");

                DataTable table = dataSet.Tables["Books"]!;

                // Create a new row with the DataTable's schema
                DataRow newRow = table.NewRow();
                newRow["Title"] = book.Title;
                newRow["Author"] = book.Author;
                newRow["Genre"] = book.Genre;
                newRow["Price"] = book.Price;
                newRow["Stock"] = book.Stock;
                table.Rows.Add(newRow);

                // Sync new row to database
                adapter.Update(dataSet, "Books");

                Console.WriteLine($"  Book '{book.Title}' added via DataSet.");
            }
        }

        // ----------------------------------------------------------
        // Helper: map a SqlDataReader row to a Book object
        // ----------------------------------------------------------
        private static Book MapReaderToBook(SqlDataReader reader)
        {
            return new Book
            {
                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Author = reader.GetString(reader.GetOrdinal("Author")),
                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                CreatedAt = reader.IsDBNull(reader.GetOrdinal("CreatedAt"))
                            ? DateTime.MinValue
                            : reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }

        // ----------------------------------------------------------
        // Helper: input validation (sanitize before any DB operation)
        // ----------------------------------------------------------
        private static void ValidateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Author cannot be empty.");
            if (string.IsNullOrWhiteSpace(book.Genre))
                throw new ArgumentException("Genre cannot be empty.");
            if (book.Price < 0)
                throw new ArgumentException("Price cannot be negative.");
            if (book.Stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            // Sanitize: trim whitespace from string fields
            book.Title = book.Title.Trim();
            book.Author = book.Author.Trim();
            book.Genre = book.Genre.Trim();
        }
    }
}