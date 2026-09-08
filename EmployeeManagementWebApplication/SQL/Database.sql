USE EmployeeDB5;

CREATE TABLE Employee(
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(30) NOT NULL,
Email VARCHAR(30) NOT NULL UNIQUE,
Department VARCHAR(30) NOT NULL,
Salary DECIMAL(10,2) NOT NULL,
IsActive BIT NOT NULL DEFAULT 1,

);

SELECT * FROM Employee;

GO

ALTER PROCEDURE GetAllEmployees
AS 
BEGIN
SELECT Id,Name,Email,Department,Salary,IsActive FROM Employee WHERE IsActive=1
END


GO


ALTER PROCEDURE GetEmployeeById
@Id INT 
AS
BEGIN
IF NOT EXISTS(SELECT 1 FROM Employee WHERE Id=@Id)
BEGIN
SELECT 204 AS StatusCode,'Id is not found'AS Message
RETURN
END

SELECT Id,Name,Email,Department,Salary ,IsActive FROM Employee WHERE Id=@Id AND IsActive=1
END


GO


ALTER PROCEDURE InsertEmployee
@Name VARCHAR(30) ,@Email VARCHAR(30)  ,@Department VARCHAR(30) ,@Salary DECIMAL(10,2) 
AS
BEGIN

IF EXISTS(SELECT 1 FROM Employee WHERE Email=@Email )
BEGIN
SELECT 209 AS StatusCode,'Email is already present' AS Message
RETURN
END
INSERT INTO Employee(Name,Email,Department,Salary) VALUES(@Name,@Email,@Department,@Salary)

SELECT 200 AS StatusCode,'Insert Successfull'AS Message
RETURN
END


GO


ALTER PROCEDURE UpdateEmployee
@Id INT ,@Name VARCHAR(30)  ,@Email VARCHAR(30)  ,@Department VARCHAR(30),@Salary DECIMAL(10,2) 
AS
BEGIN

IF EXISTS(SELECT 1 FROM Employee WHERE Email=@Email AND Id<>@Id)
BEGIN
SELECT 209 AS StatusCode,'Email is already present' AS Message
RETURN
END

IF NOT EXISTS(SELECT 1 FROM Employee WHERE Id=@Id)
BEGIN
SELECT 204 AS StatusCode,'Id is not found'AS Message
RETURN
END

UPDATE Employee
SET Name=@Name,Email=@Email,Department=@Department,Salary=@Salary
WHERE Id=@Id AND IsActive=1

SELECT 201 AS StatusCode,'Update successfull' AS Message
RETURN
END


GO


ALTER PROCEDURE DeleteEmployee
@Id INT 
AS
BEGIN

IF NOT EXISTS(SELECT 1 FROM Employee WHERE Id=@Id)
BEGIN
SELECT 204 AS StatusCode,'Id is not found'AS Message
RETURN
END

UPDATE Employee  SET IsActive=0 WHERE Id=@Id AND IsActive=1

SELECT 201 AS StatusCode,'Delete Successfull' AS Message
RETURN
END


