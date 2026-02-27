-- CREATE DATABASE Employeedb;
-- USE Employeedb;

-- CREATE TABLE Address (
--     address_id INT PRIMARY KEY IDENTITY(1, 1),
--     street VARCHAR(255),
--     city VARCHAR(100),
--     state VARCHAR(100),
--     postal_code VARCHAR(20)
-- );

-- CREATE TABLE Employee (
--     employee_id INT PRIMARY KEY IDENTITY(1,1),
--     first_name VARCHAR(100),
--     last_name VARCHAR(100),
--     email VARCHAR(100),
--     address_id INT REFERENCES Address(address_id)
-- );


-- INSERT INTO Address VALUES 
-- ('1234 Elm Street', 'Springfield', 'Illnois', '62704'),
-- ('5678 Oak Street', 'Decatur', 'Alabama', '35601'),
-- ('123 Patia', 'BBSR', 'India', '755019'),
-- ('123 Patia', 'BBSR', 'India', '755019');

-- INSERT INTO Employee VALUES 
-- ('John', 'Doe', 'johndoe@exmaple.com', 1),
-- ('Jane', 'Doe', 'janedoe@example.com', 2),
-- ('Ramesh', 'Sharma', 'ramesh@exmaple.com', 3),
-- ('Ramesh', 'Sharma', 'ramesh@exmaple.com', 4);



CREATE PROCEDURE [dbo].[CreateEmployeeWithAddress]
    @first_name VARCHAR(100),
    @last_name  VARCHAR(100),
    @email     VARCHAR(100),
    @street    VARCHAR(255),
    @city      VARCHAR(100),
    @state     VARCHAR(100),
    @postal_code VARCHAR(20)
AS
BEGIN
    DECLARE @AddressID INT;

    -- Insert into Address table
    INSERT INTO Address (Street, City, State, postal_code)
    VALUES (@street, @city, @state, @postal_code);

    -- Get generated AddressID
    SET @AddressID = SCOPE_IDENTITY();

    -- Insert into Employee table
    INSERT INTO Employee (first_name, last_name, email, address_id)
    VALUES (@first_name, @last_name, @email, @AddressID);
END;
GO

EXEC CreateEmployeeWithAddress 'Parth', 'Saini', 'saini@exmaple.com', 'oxford street', 'Ambala', 'Harayana', '133001';



CREATE PROCEDURE [dbo].[DeleteEmployee]
    @EmployeeID INT
AS
BEGIN
    DECLARE @AddressID INT;

    BEGIN TRANSACTION;

    -- Get AddressID
    SELECT @AddressID = address_id
    FROM Employee
    WHERE employee_id = @EmployeeID;

    -- Delete Employee
    DELETE FROM Employee
    WHERE employee_id = @EmployeeID;

    -- Delete Address
    DELETE FROM Address
    WHERE address_id = @AddressID;

    COMMIT TRANSACTION;
END;
GO

EXEC DeleteEmployee 5;
select * from Employee;
select * from Address;




CREATE PROCEDURE [dbo].[GetEmployeeByID]
    @EmployeeID INT
AS
BEGIN
    SELECT
        e.employee_id,
        e.first_name,
        e.last_name,
        e.Email,
        a.Street,
        a.City,
        a.State,
        a.postal_code
    FROM Employee e
    INNER JOIN Address a
        ON e.address_id = a.address_id
    WHERE e.employee_id = @EmployeeID;
END;
GO

EXEC GetEmployeeByID 3;



CREATE PROCEDURE [dbo].[GetAllEmployees]
AS
BEGIN
    SELECT
        e.employee_id,
        e.first_name,
        e.last_name,
        e.Email,
        a.Street,
        a.City,
        a.State,
        a.postal_code
    FROM Employee e
    INNER JOIN Address a
        ON e.address_id = a.address_id;
END;
GO

EXEC GetAllEmployees;



CREATE PROCEDURE [dbo].[UpdateEmployeeWithAddress]
    @EmployeeID INT,
    @FirstName VARCHAR(100),
    @LastName  VARCHAR(100),
    @Email     VARCHAR(100),
    @Street    VARCHAR(255),
    @City      VARCHAR(100),
    @State     VARCHAR(100),
    @PostalCode VARCHAR(20),
    @AddressID INT
AS
BEGIN
    -- Update Address
    UPDATE Address
    SET
        Street = @Street,
        City = @City,
        State = @State,
        postal_code = @PostalCode
    WHERE address_id = @AddressID;

    -- Update Employee
    UPDATE Employee
    SET
        first_name = @FirstName,
        last_name  = @LastName,
        email     = @Email,
        address_id = @AddressID
    WHERE employee_id = @EmployeeID;
END;
GO

EXEC UpdateEmployeeWithAddress 1, 'Parth', 'Saini', 'parth@gmail.com', 'Sunaria', 'Rohtak', 'Haryana', '133001', 1;

select * from Employee;