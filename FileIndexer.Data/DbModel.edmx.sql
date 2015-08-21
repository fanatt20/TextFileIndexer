
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/21/2015 15:40:42
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


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

-- Creating table 'PathToTextFiles'
CREATE TABLE [dbo].[PathToTextFiles] (
    [Id] int IDENTITY(1,1) NOT NULL
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

-- Creating primary key on [Id] in table 'PathToTextFiles'
ALTER TABLE [dbo].[PathToTextFiles]
ADD CONSTRAINT [PK_PathToTextFiles]
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
    REFERENCES [dbo].[PathToTextFiles]
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