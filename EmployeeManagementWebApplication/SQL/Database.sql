USE EmployeeDB7;



CREATE TABLE Roles(
RoleId INT PRIMARY KEY,
RoleName VARCHAR(40) NOT NULL UNIQUE
);

INSERT INTO Roles(RoleId, RoleName)
VALUES
(1, 'User'),
(2, 'Admin');



CREATE TABLE Users
(
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(50) NOT NULL ,
Email VARCHAR(100) NOT NULL UNIQUE ,
Password VARCHAR(255) NOT NULL,
RoleId INT NOT NULL,
    
CONSTRAINT FK_Users_Roles
FOREIGN KEY (RoleId)
REFERENCES Roles(RoleId)
);


CREATE TABLE Employee1(
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(30) NOT NULL,
Email VARCHAR(100) NOT NULL UNIQUE,
ProfileImage VARCHAR(500),
Department VARCHAR(30) NOT NULL,
Salary DECIMAL(10,2) NOT NULL,
IsActive BIT NOT NULL DEFAULT 1,
UserId INT NULL,

CONSTRAINT FK_Employee1_Users
FOREIGN KEY (UserId)
REFERENCES Users(Id)


);

GO
GO


SELECT * FROM Users;

SELECT * FROM Employee1;

GO

CREATE PROCEDURE GetAllEmployees
AS 
BEGIN
SELECT Id,Name,Email,Department,Salary,IsActive,ProfileImage FROM Employee1 WHERE IsActive=1
END


GO

CREATE PROCEDURE GetEmployeeById
@Id INT 
AS
BEGIN
IF NOT EXISTS(SELECT 1 FROM Employee1 WHERE Id=@Id)
BEGIN
SELECT 204 AS StatusCode,'Id is not found'AS Message
RETURN
END

SELECT 
Id,Name,Email,Department,Salary ,IsActive,ProfileImage 
FROM Employee1 
WHERE Id=@Id AND IsActive=1
END


GO


CREATE PROCEDURE InsertEmployee
@Name VARCHAR(30) ,@Email VARCHAR(100)  ,@Department VARCHAR(30) ,@Salary DECIMAL(10,2) 
AS
BEGIN

IF EXISTS(SELECT 1 FROM Employee1 WHERE Email=@Email )
BEGIN
SELECT 209 AS StatusCode,'Email is already present' AS Message
RETURN
END
INSERT INTO Employee1(Name,Email,Department,Salary) 
VALUES(@Name,@Email,@Department,@Salary)


SELECT 200 AS StatusCode,'Insert Successful'AS Message
RETURN
END


GO


CREATE PROCEDURE UpdateEmployee
@Id INT,@Name VARCHAR(30),@Email VARCHAR(100),@Department VARCHAR(30),@Salary DECIMAL(10,2) 
AS
BEGIN

IF EXISTS(SELECT 1 FROM Employee1 WHERE Email=@Email AND Id<>@Id)
BEGIN
SELECT 400 AS StatusCode,'Email is already present' AS Message
RETURN
END

IF NOT EXISTS(SELECT 1 FROM Employee1 WHERE Id=@Id)
BEGIN
SELECT 400 AS StatusCode,'Id is not found'AS Message
RETURN
END

UPDATE Employee1
SET Name=@Name,Email=@Email,Department=@Department,Salary=@Salary
WHERE Id=@Id AND IsActive=1

SELECT 200 AS StatusCode,'Update successfull' AS Message
RETURN
END


GO


CREATE PROCEDURE DeleteEmployee
@Id INT 
AS
BEGIN

IF NOT EXISTS(SELECT 1 FROM Employee1 WHERE Id=@Id)
BEGIN
SELECT 400 AS StatusCode,'Id is not found'AS Message
RETURN
END

UPDATE Employee1  SET IsActive=0 WHERE Id=@Id AND IsActive=1

SELECT 200 AS StatusCode,'Delete Successfull' AS Message
RETURN
END

GO


GO

CREATE PROCEDURE UpdateEmployeeProfileImage
@Id INT,
@ProfileImage VARCHAR(500)
AS
BEGIN
UPDATE Employee1
SET ProfileImage=@ProfileImage
WHERE Id=@Id AND IsActive=1
END


SELECT * FROM Employee1;


GO

CREATE PROCEDURE RegisterUser
@Name VARCHAR(50),
@Password VARCHAR(250),
@Email VARCHAR(100),
@RoleName VARCHAR(20)
AS
BEGIN
SET NOCOUNT ON

DECLARE @RoleId INT;

SELECT @RoleId=RoleId
FROM Roles
WHERE RoleName=@RoleName

IF @RoleId IS NULL
BEGIN
SELECT 400 AS StatusCode,'Invalid Role Name' AS Message
RETURN;
END;

IF EXISTS (
SELECT 1 FROM Users
WHERE Email=@Email
)
BEGIN
SELECT 400 AS StatusCode,'Email is already exists'AS Message
RETURN
END

INSERT INTO Users(Name,Password,Email,RoleId) VALUES(@Name,@Password,@Email,@RoleId);
SELECT 200 AS StatusCode,'Insert Successfull' AS Message
END

GO

CREATE PROCEDURE LoginUser
@Email VARCHAR(100),
@Password VARCHAR(250)
AS
BEGIN

SET NOCOUNT ON

SELECT U.Id,U.Name,U.Email,U.Password,U.RoleId,R.RoleName FROM Users U
INNER JOIN Roles R
ON R.RoleId=U.RoleId
WHERE U.Email=@Email AND U.Password=@Password
END


GO

CREATE PROCEDURE LinkEmployeeToUser
@EmployeeId INT,
@UserId INT
AS
BEGIN
SET NOCOUNT ON;

IF NOT EXISTS(
SELECT 1 FROM Users
WHERE Id = @UserId
)
BEGIN
SELECT 400 AS StatusCode, 'User does not exist' AS Message;
RETURN;
END;

IF NOT EXISTS(
SELECT 1 FROM Employee1
WHERE Id = @EmployeeId
)
BEGIN
SELECT 400 AS StatusCode,'Employee does not exist' AS Message;
RETURN;
END;

IF EXISTS(
SELECT 1 FROM Employee1
WHERE UserId = @UserId
)
BEGIN
SELECT 400 AS StatusCode,'User is already linked to an employee' AS Message;
RETURN;
END;

UPDATE Employee1
SET UserId = @UserId
WHERE Id = @EmployeeId;

SELECT 200 AS StatusCode,'Employee linked successfully' AS Message;
END;


GO

CREATE PROCEDURE GetUserProfile
@UserId INT
AS
BEGIN
SET NOCOUNT ON;

SELECT Id,Name,Email,ProfileImage,UserId FROM Employee1
WHERE UserId = @UserId;
END;
GO

CREATE PROCEDURE UpdateUserProfile
@UserId INT,
@Name VARCHAR(50),
@Email VARCHAR(100)
AS
BEGIN
SET NOCOUNT ON;

IF NOT EXISTS(
SELECT 1 FROM Employee1
WHERE UserId = @UserId
)
BEGIN
SELECT 400 AS StatusCode,'Employee profile not found' AS Message;
RETURN;
END;

IF EXISTS(
SELECT 1 FROM Employee1
WHERE Email = @Email
AND UserId <> @UserId
)
BEGIN
SELECT 400 AS StatusCode,'Email already exists' AS Message;
RETURN;
END;

UPDATE Employee1
SET Name = @Name,Email = @Email
WHERE UserId = @UserId;

SELECT 200 AS StatusCode,'Profile updated successfully' AS Message;
END;
GO

SELECT * FROM Users;

SELECT Id, Name, Email, RoleId
FROM Users
WHERE Email IN ('testuser@gmail.com', 'testadmin@gmail.com');

SELECT Id, Name, Email, UserId
FROM Employee1;

EXEC sp_helptext 'UpdateEmployee';

SELECT DB_NAME() AS CurrentDatabase;

SELECT Id, Name, Email, IsActive
FROM Employee1
WHERE Id = 1;

EXEC UpdateEmployee
    @Id = 1,
    @Name = 'Admin Updated Employee',
    @Email = 'adminupdated@gmail.com',
    @Department = 'HR',
    @Salary = 35000;

    SELECT Id, Name, Email, IsActive
FROM Employee1;

SELECT *
FROM Employee1
WHERE Email = 'adminupdated@gmail.com';

EXEC sp_helptext 'UpdateEmployee';

SELECT OBJECT_DEFINITION(OBJECT_ID('UpdateEmployee'));

SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.UpdateEmployee'));
