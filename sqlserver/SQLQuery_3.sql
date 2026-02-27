-- CREATE DATABASE UniversityDB;
-- USE UniversityDB;
 
-- -- Departments Table
-- -- Insert Departments
-- INSERT INTO Departments (DeptName) VALUES ('Computer Science'), ('Mathematics'), ('Physics');
 
-- -- Insert Courses
-- INSERT INTO Courses (CourseName, DeptId) VALUES
-- ('Data Structures', 1),
-- ('Algorithms', 1),
-- ('Linear Algebra', 2),
-- ('Quantum Mechanics', 3);
 
-- -- Insert Students
-- INSERT INTO Students (FirstName, LastName, Email, DeptId) VALUES
-- ('Alice', 'Johnson', 'alice@uni.com', 1),
-- ('Bob', 'Smith', 'bob@uni.com', 2),
-- ('Charlie', 'Brown', 'charlie@uni.com', 3);
 
-- -- Insert Enrollments
-- INSERT INTO Enrollments (StudentId, CourseId, Grade) VALUES
-- (1, 1, 'A'),
-- (1, 2, 'B'),
-- (2, 3, 'A'),
-- (3, 4, 'C');

CREATE PROCEDURE sp_InsertDepartment
    @DeptName NVARCHAR(100)
AS
BEGIN
    INSERT INTO Departments(DeptName)
    VALUES(@DeptName);
END

CREATE PROCEDURE sp_GetAllDepartments
AS
BEGIN
    SELECT * FROM Departments;
END

CREATE PROCEDURE sp_GetDepartmentById
    @DeptId INT
AS
BEGIN
    SELECT * 
    FROM Departments
    WHERE DeptId = @DeptId;
END

CREATE PROCEDURE sp_UpdateDepartment
    @DeptId INT,
    @DeptName NVARCHAR(100)
AS
BEGIN
    UPDATE Departments
    SET DeptName = @DeptName
    WHERE DeptId = @DeptId;
END

CREATE PROCEDURE sp_DeleteDepartment
    @DeptId INT
AS
BEGIN
    DELETE FROM Departments
    WHERE DeptId = @DeptId;
END

CREATE PROCEDURE sp_InsertStudent
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @DeptId INT
AS
BEGIN
    INSERT INTO Students(FirstName, LastName, Email, DeptId)
    VALUES(@FirstName, @LastName, @Email, @DeptId);
END

CREATE PROCEDURE sp_GetAllStudents
AS
BEGIN
    SELECT 
        S.StudentId,
        S.FirstName,
        S.LastName,
        S.Email,
        D.DeptName
    FROM Students S
    INNER JOIN Departments D
        ON S.DeptId = D.DeptId;
END

CREATE PROCEDURE sp_GetStudentById
    @StudentId INT
AS
BEGIN
    SELECT *
    FROM Students
    WHERE StudentId = @StudentId;
END

CREATE PROCEDURE sp_UpdateStudent
    @StudentId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @DeptId INT
AS
BEGIN
    UPDATE Students
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        DeptId = @DeptId
    WHERE StudentId = @StudentId;
END

CREATE PROCEDURE sp_DeleteStudent
    @StudentId INT
AS
BEGIN
    DELETE FROM Students
    WHERE StudentId = @StudentId;
END

CREATE PROCEDURE sp_InsertCourse
    @CourseName NVARCHAR(100),
    @DeptId INT
AS
BEGIN
    INSERT INTO Courses(CourseName, DeptId)
    VALUES(@CourseName, @DeptId);
END

CREATE PROCEDURE sp_GetAllCourses
AS
BEGIN
    SELECT 
        C.CourseId,
        C.CourseName,
        D.DeptName
    FROM Courses C
    INNER JOIN Departments D
        ON C.DeptId = D.DeptId;
END

CREATE PROCEDURE sp_GetCourseById
    @CourseId INT
AS
BEGIN
    SELECT *
    FROM Courses
    WHERE CourseId = @CourseId;
END

CREATE PROCEDURE sp_UpdateCourse
    @CourseId INT,
    @CourseName NVARCHAR(100),
    @DeptId INT
AS
BEGIN
    UPDATE Courses
    SET CourseName = @CourseName,
        DeptId = @DeptId
    WHERE CourseId = @CourseId;
END

CREATE PROCEDURE sp_DeleteCourse
    @CourseId INT
AS
BEGIN
    DELETE FROM Courses
    WHERE CourseId = @CourseId;
END

CREATE PROCEDURE sp_InsertEnrollment
    @StudentId INT,
    @CourseId INT,
    @Grade CHAR(2)
AS
BEGIN
    INSERT INTO Enrollments(StudentId, CourseId, Grade)
    VALUES(@StudentId, @CourseId, @Grade);
END

CREATE PROCEDURE sp_GetAllEnrollments
AS
BEGIN
    SELECT 
        E.EnrollmentId,
        S.FirstName + ' ' + S.LastName AS StudentName,
        C.CourseName,
        E.Grade
    FROM Enrollments E
    INNER JOIN Students S ON E.StudentId = S.StudentId
    INNER JOIN Courses C ON E.CourseId = C.CourseId;
END

CREATE PROCEDURE sp_GetEnrollmentById
    @EnrollmentId INT
AS
BEGIN
    SELECT *
    FROM Enrollments
    WHERE EnrollmentId = @EnrollmentId;
END

CREATE PROCEDURE sp_UpdateEnrollment
    @EnrollmentId INT,
    @StudentId INT,
    @CourseId INT,
    @Grade CHAR(2)
AS
BEGIN
    UPDATE Enrollments
    SET StudentId = @StudentId,
        CourseId = @CourseId,
        Grade = @Grade
    WHERE EnrollmentId = @EnrollmentId;
END

CREATE PROCEDURE sp_DeleteEnrollment
    @EnrollmentId INT
AS
BEGIN
    DELETE FROM Enrollments
    WHERE EnrollmentId = @EnrollmentId;
END