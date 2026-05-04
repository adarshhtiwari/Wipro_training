Create DATABASE MyDatabase;
USE MyDatabase;

CREATE TABLE Employees
(
    EmpID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary INT
);

INSERT INTO Employees VALUES (1, 'John', 'Doe', 'IT', 50000);
INSERT INTO Employees VALUES (2, 'Jane', 'Smith', 'HR', 60000);
INSERT INTO Employees VALUES (3, 'Amit', 'Sharma', 'IT', 80000);
INSERT INTO Employees VALUES (4, 'Sara', 'Khan', 'Finance', 75000);

CREATE FUNCTION GetEmployeeFullName (
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50)
)
RETURNS VARCHAR(100)
AS
BEGIN     RETURN CONCAT(@FirstName, ' ', @LastName);
END;

SELECT 
    dbo.GetEmployeeFullName(FirstName, LastName) AS FullName,
    Department,
    Salary
FROM Employees;

CREATE FUNCTION dbo.GetEmployeeDepartment (
    @Dept VARCHAR(50)
)
RETURNS TABLE
AS

RETURN
(
    SELECT EmpID, FirstName, LastName, Salary, Department
    FROM Employees
    WHERE Department = @Dept
);

SELECT * FROM dbo.GetEmployeeDepartment('IT');

SELECT * FROM dbo.GetEmployeeDepartment('HR');

CREATE FUNCTION dbo.GetEmployeeAnnualSalary (
    @Salary INT
)
RETURNS INT
AS
BEGIN
    RETURN @Salary * 12;
END;

SELECT 
    dbo.GetEmployeeFullName(FirstName, LastName) AS FullName,
    Department,
    dbo.GetEmployeeAnnualSalary(Salary) AS AnnualSalary
FROM Employees;



CREATE FUNCTION dbo.GetEmployeeBySalary (
    @MINSalary INT
)
RETURNS TABLE
as 
RETURN
(
    SELECT EmpID,  dbo.GetEmployeeFullName(FirstName, LastName) AS FullName, Salary, Department
    FROM Employees
    WHERE Salary > @MINSalary
);

SELECT *
FROM dbo.GetEmployeeBySalary(60000);

ALTER TABLE Employees ADD Bonus INT;

SELECT * FROM Employees;

UPDATE Employees
SET BONUS = CASE
                WHEN Department = 'IT' THEN Salary * 0.15
                WHEN Department = 'HR' THEN Salary * 0.12
                ELSE Salary * 0.10
            END;

SELECT 
    dbo.GetEmployeeFullName(FirstName, LastName) AS FullName,
    Department,
    Salary,
    dbo.GetEmployeeAnnualSalary(Salary) AS AnnualSalary,
    Bonus
    
FROM Employees;


--limitation of Functions : 
--1) Function are majorly used for perfoming SELECT operations 
--2) For perfoming CRUD we have to use StoredProcedures or triggers 
--3) Since they dont performing CRUD but they can be used for :
        --1)Validating Data before insert 
        --2)Calculating Values during INSERT/UPDATE
        --3)Filtering record during select


--Best practice for impleting functions:
    --1)Use Inline TVF function insted of MultiStatment
            --As They offer better performance
            --They are better optimised for SQL Server.
    --2)Avoid Scalar function in large Queries.
            --They can slow down performace( because of its Row by Row execution nature)
    --3)Keep function Deterministic
        -- Same input should give same output
    --4)Use prefix Schema (ex.dbo)
        --We can have a default Schema with every function declaration
    --5)Use Function for reading logic 
        -- use Procedure for CRUD operations