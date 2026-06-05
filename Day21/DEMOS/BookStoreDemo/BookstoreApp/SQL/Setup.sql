
DROP DATABASE IF EXISTS BookstoreDB;
-- 1. Create and use the database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BookstoreDB')
    CREATE DATABASE BookstoreDB;
GO

USE BookstoreDB;
GO

-- 2. Create the Books table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books')
BEGIN
    CREATE TABLE Books (
        BookId    INT IDENTITY(1,1) PRIMARY KEY,
        Title     NVARCHAR(200)     NOT NULL,
        Author    NVARCHAR(100)     NOT NULL,
        Genre     NVARCHAR(50)      NOT NULL,
        Price     DECIMAL(10,2)     NOT NULL,
        Stock     INT               NOT NULL DEFAULT 0,
        CreatedAt DATETIME          NOT NULL DEFAULT GETDATE()
    );

    -- Seed some sample data
    INSERT INTO Books (Title, Author, Genre, Price, Stock) VALUES
        ('The Pragmatic Programmer', 'Andy Hunt',        'Technology', 599.00, 10),
        ('Clean Code',              'Robert C. Martin',  'Technology', 549.00, 15),
        ('The Alchemist',           'Paulo Coelho',      'Fiction',    299.00, 25),
        ('Atomic Habits',           'James Clear',       'Self-Help',  349.00, 20),
        ('Sapiens',                 'Yuval Noah Harari', 'History',    399.00, 12);

    PRINT 'Books table created and seeded.';
END
ELSE
    PRINT 'Books table already exists.';
GO



-- SP: Get all books
IF OBJECT_ID('sp_GetAllBooks', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllBooks;
GO
CREATE PROCEDURE sp_GetAllBooks
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BookId, Title, Author, Genre, Price, Stock, CreatedAt
    FROM Books
    ORDER BY BookId;
END
GO

-- SP: Get a single book by ID
IF OBJECT_ID('sp_GetBookById', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetBookById;
GO
CREATE PROCEDURE sp_GetBookById
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BookId, Title, Author, Genre, Price, Stock, CreatedAt
    FROM Books
    WHERE BookId = @BookId;
END
GO

-- SP: Add a new book
IF OBJECT_ID('sp_AddBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_AddBook;
GO
CREATE PROCEDURE sp_AddBook
    @Title  NVARCHAR(200),
    @Author NVARCHAR(100),
    @Genre  NVARCHAR(50),
    @Price  DECIMAL(10,2),
    @Stock  INT,
    @NewBookId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Books (Title, Author, Genre, Price, Stock)
    VALUES (@Title, @Author, @Genre, @Price, @Stock);

    SET @NewBookId = SCOPE_IDENTITY();
END
GO

-- SP: Update an existing book
IF OBJECT_ID('sp_UpdateBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateBook;
GO
CREATE PROCEDURE sp_UpdateBook
    @BookId INT,
    @Title  NVARCHAR(200),
    @Author NVARCHAR(100),
    @Genre  NVARCHAR(50),
    @Price  DECIMAL(10,2),
    @Stock  INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Books
    SET Title  = @Title,
        Author = @Author,
        Genre  = @Genre,
        Price  = @Price,
        Stock  = @Stock
    WHERE BookId = @BookId;

    IF @@ROWCOUNT = 0
        RAISERROR('Book with ID %d not found.', 16, 1, @BookId);
END
GO

-- SP: Delete a book
IF OBJECT_ID('sp_DeleteBook', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteBook;
GO
CREATE PROCEDURE sp_DeleteBook
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Books WHERE BookId = @BookId;

    IF @@ROWCOUNT = 0
        RAISERROR('Book with ID %d not found.', 16, 1, @BookId);
END
GO

PRINT 'All stored procedures created successfully.';
GO