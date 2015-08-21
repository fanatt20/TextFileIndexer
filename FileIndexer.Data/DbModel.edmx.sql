
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/21/2015 17:27:37
-- Generated from EDMX file: C:\Users\Андрей\Documents\GitHub\TextFileIndexer\FileIndexer.Data\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TextFilesAndWordsDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_WordsDictionaryWords]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Words] DROP CONSTRAINT [FK_WordsDictionaryWords];
GO
IF OBJECT_ID(N'[dbo].[FK_PathToTextFileTextFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TextFiles] DROP CONSTRAINT [FK_PathToTextFileTextFile];
GO
IF OBJECT_ID(N'[dbo].[FK_TextFileWords]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Words] DROP CONSTRAINT [FK_TextFileWords];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TextFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TextFiles];
GO
IF OBJECT_ID(N'[dbo].[PathToTextFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PathToTextFiles];
GO
IF OBJECT_ID(N'[dbo].[Words]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Words];
GO
IF OBJECT_ID(N'[dbo].[WordsDictionaries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WordsDictionaries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TextFiles'
CREATE TABLE [dbo].[TextFiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PathToTextFileId] int  NOT NULL
);
GO

-- Creating table 'PathToTextFileCollections'
CREATE TABLE [dbo].[PathToTextFileCollections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [PreviousDirectory] int  NULL
);
GO

-- Creating table 'Words'
CREATE TABLE [dbo].[Words] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WordsDictionaryId] int  NOT NULL,
    [TextFileId] int  NOT NULL,
    [RowPosition] int  NOT NULL,
    [ColumnPosition] int  NOT NULL
);
GO

-- Creating table 'WordsDictionaries'
CREATE TABLE [dbo].[WordsDictionaries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TextFiles'
ALTER TABLE [dbo].[TextFiles]
ADD CONSTRAINT [PK_TextFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PathToTextFileCollections'
ALTER TABLE [dbo].[PathToTextFileCollections]
ADD CONSTRAINT [PK_PathToTextFileCollections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Words'
ALTER TABLE [dbo].[Words]
ADD CONSTRAINT [PK_Words]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WordsDictionaries'
ALTER TABLE [dbo].[WordsDictionaries]
ADD CONSTRAINT [PK_WordsDictionaries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [WordsDictionaryId] in table 'Words'
ALTER TABLE [dbo].[Words]
ADD CONSTRAINT [FK_WordsDictionaryWords]
    FOREIGN KEY ([WordsDictionaryId])
    REFERENCES [dbo].[WordsDictionaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WordsDictionaryWords'
CREATE INDEX [IX_FK_WordsDictionaryWords]
ON [dbo].[Words]
    ([WordsDictionaryId]);
GO

-- Creating foreign key on [PathToTextFileId] in table 'TextFiles'
ALTER TABLE [dbo].[TextFiles]
ADD CONSTRAINT [FK_PathToTextFileTextFile]
    FOREIGN KEY ([PathToTextFileId])
    REFERENCES [dbo].[PathToTextFileCollections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PathToTextFileTextFile'
CREATE INDEX [IX_FK_PathToTextFileTextFile]
ON [dbo].[TextFiles]
    ([PathToTextFileId]);
GO

-- Creating foreign key on [TextFileId] in table 'Words'
ALTER TABLE [dbo].[Words]
ADD CONSTRAINT [FK_TextFileWords]
    FOREIGN KEY ([TextFileId])
    REFERENCES [dbo].[TextFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TextFileWords'
CREATE INDEX [IX_FK_TextFileWords]
ON [dbo].[Words]
    ([TextFileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------