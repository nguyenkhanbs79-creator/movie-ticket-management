IF DB_ID('CinemaDb') IS NULL
BEGIN
    CREATE DATABASE CinemaDb;
END
GO

USE CinemaDb;
GO

IF OBJECT_ID('dbo.Showtimes', 'U') IS NOT NULL DROP TABLE dbo.Showtimes;
IF OBJECT_ID('dbo.Auditoriums', 'U') IS NOT NULL DROP TABLE dbo.Auditoriums;
IF OBJECT_ID('dbo.Movies', 'U') IS NOT NULL DROP TABLE dbo.Movies;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Movies
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Genre NVARCHAR(100) NULL,
    Duration INT NOT NULL,
    AgeRating NVARCHAR(20) NULL
);

CREATE TABLE dbo.Auditoriums
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    SeatCount INT NOT NULL
);

CREATE TABLE dbo.Showtimes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT NOT NULL,
    AuditoriumId INT NOT NULL,
    StartTime DATETIME2 NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Showtimes_Movies FOREIGN KEY (MovieId) REFERENCES dbo.Movies(Id),
    CONSTRAINT FK_Showtimes_Auditoriums FOREIGN KEY (AuditoriumId) REFERENCES dbo.Auditoriums(Id)
);

CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    FullName NVARCHAR(100) NULL
);
GO

INSERT INTO dbo.Movies (Title, Genre, Duration, AgeRating)
VALUES (N'Inception', N'Sci-Fi', 148, N'PG-13'),
       (N'Spirited Away', N'Animation', 125, N'PG'),
       (N'Parasite', N'Thriller', 132, N'R');

INSERT INTO dbo.Auditoriums (Name, SeatCount)
VALUES (N'Room A', 120),
       (N'Room B', 80);

INSERT INTO dbo.Users (Username, PasswordHash, FullName)
VALUES (N'admin', N'hashedpassword1', N'System Administrator'),
       (N'manager', N'hashedpassword2', N'Cinema Manager');

INSERT INTO dbo.Showtimes (MovieId, AuditoriumId, StartTime, BasePrice)
VALUES (1, 1, SYSDATETIME(), 8.50),
       (2, 2, DATEADD(HOUR, 3, SYSDATETIME()), 7.00),
       (3, 1, DATEADD(DAY, 1, SYSDATETIME()), 9.00);
GO

CREATE OR ALTER VIEW dbo.v_ShowtimeDetails AS
SELECT s.Id,
       m.Title,
       a.Name AS Auditorium,
       s.StartTime,
       s.BasePrice
FROM dbo.Showtimes AS s
JOIN dbo.Movies AS m ON m.Id = s.MovieId
JOIN dbo.Auditoriums AS a ON a.Id = s.AuditoriumId;
GO

CREATE OR ALTER PROCEDURE dbo.sp_SearchShowtimes
    @TitleLike NVARCHAR(200) = NULL,
    @From DATETIME2 = NULL,
    @To DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id,
           Title,
           Auditorium,
           StartTime,
           BasePrice
    FROM dbo.v_ShowtimeDetails
    WHERE (@TitleLike IS NULL OR @TitleLike = N'' OR Title LIKE N'%' + @TitleLike + N'%')
      AND (@From IS NULL OR StartTime >= @From)
      AND (@To IS NULL OR StartTime < DATEADD(DAY, 1, @To))
    ORDER BY StartTime;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_RevenueByDate
    @From DATETIME2,
    @To DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CAST(StartTime AS DATE) AS [Date],
           SUM(BasePrice) AS Revenue
    FROM dbo.Showtimes
    WHERE StartTime >= @From
      AND StartTime < DATEADD(DAY, 1, @To)
    GROUP BY CAST(StartTime AS DATE)
    ORDER BY [Date];
END;
GO
