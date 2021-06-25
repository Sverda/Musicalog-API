
/* DATABASE */
IF NOT EXISTS(SELECT *
FROM sys.databases
WHERE name = 'Musicalog')
  BEGIN
    CREATE DATABASE [Musicalog]
END;
GO

USE [Musicalog]
GO


/* TABLES */
IF NOT EXISTS (SELECT *
FROM sysobjects
WHERE name='Artists' and xtype='U')
BEGIN
    CREATE TABLE Artists
    (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(100) NOT NULL
    )
END

IF NOT EXISTS (SELECT *
FROM sysobjects
WHERE name='Albums' and xtype='U')
BEGIN
    CREATE TABLE Albums
    (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Title VARCHAR(100) NOT NULL,
        ArtistId INT NOT NULL,
        Type INT NOT NULL,
        Stock INT NOT NULL,
        CONSTRAINT FK_Album_Artists FOREIGN KEY (ArtistId) REFERENCES dbo.Artists (Id) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
    )
END

/* ARTISTS */
SET IDENTITY_INSERT dbo.Artists ON;
GO

IF NOT EXISTS (SELECT *
FROM dbo.Artists
WHERE Name = 'Bob Dylan') BEGIN
    INSERT INTO dbo.Artists
        (Id, Name)
    VALUES
        (1, 'Bob Dylan')
END

IF NOT EXISTS (SELECT *
FROM dbo.Artists
WHERE Name = 'Prince and the Revolution') BEGIN
    INSERT INTO dbo.Artists
        (Id, Name)
    VALUES
        (2, 'Prince and the Revolution')
END

IF NOT EXISTS (SELECT *
FROM dbo.Artists
WHERE Name = 'Fleetwood Mac') BEGIN
    INSERT INTO dbo.Artists
        (Id, Name)
    VALUES
        (3, 'Fleetwood Mac')
END

IF NOT EXISTS (SELECT *
FROM dbo.Artists
WHERE Name = 'Nirvana') BEGIN
    INSERT INTO dbo.Artists
        (Id, Name)
    VALUES
        (4, 'Nirvana')
END

/* ALBUMS */
IF NOT EXISTS (SELECT *
FROM dbo.Albums
WHERE Title = 'Blood on the Tracks') BEGIN
    INSERT INTO dbo.Albums
        (Title, ArtistId, Type, Stock)
    VALUES
        ('Blood on the Tracks', 1, 0, 100)
END

IF NOT EXISTS (SELECT *
FROM dbo.Albums
WHERE Title = 'Purple Rain') BEGIN
    INSERT INTO dbo.Albums
        (Title, ArtistId, Type, Stock)
    VALUES
        ('Purple Rain', 2, 1, 90)
END

IF NOT EXISTS (SELECT *
FROM dbo.Albums
WHERE Title = 'Rumours') BEGIN
    INSERT INTO dbo.Albums
        (Title, ArtistId, Type, Stock)
    VALUES
        ('Rumours', 3, 0, 110)
END

IF NOT EXISTS (SELECT *
FROM dbo.Albums
WHERE Title = 'Nevermind') BEGIN
    INSERT INTO dbo.Albums
        (Title, ArtistId, Type, Stock)
    VALUES
        ('Nevermind', 4, 1, 220)
END