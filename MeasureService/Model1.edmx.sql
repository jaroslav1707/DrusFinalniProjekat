
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2017 22:55:07
-- Generated from EDMX file: C:\Users\jaros\Desktop\MeasureService\MeasureService\Model1.edmx
-- --------------------------------------------------
create database drus_nova;
SET QUOTED_IDENTIFIER OFF;
GO
USE [drus_nova];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LocationMeasuringStation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeasuringStations] DROP CONSTRAINT [FK_LocationMeasuringStation];
GO
IF OBJECT_ID(N'[dbo].[FK_MeasuringStationMeasurement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Measurements] DROP CONSTRAINT [FK_MeasuringStationMeasurement];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Measurements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Measurements];
GO
IF OBJECT_ID(N'[dbo].[MeasuringStations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MeasuringStations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Measurements'
CREATE TABLE [dbo].[Measurements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Value] float  NOT NULL,
    [Time] datetime  NOT NULL,
    [MeasuringStation_Id] int  NOT NULL
);
GO

-- Creating table 'MeasuringStations'
CREATE TABLE [dbo].[MeasuringStations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Location_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Measurements'
ALTER TABLE [dbo].[Measurements]
ADD CONSTRAINT [PK_Measurements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MeasuringStations'
ALTER TABLE [dbo].[MeasuringStations]
ADD CONSTRAINT [PK_MeasuringStations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Location_Id] in table 'MeasuringStations'
ALTER TABLE [dbo].[MeasuringStations]
ADD CONSTRAINT [FK_LocationMeasuringStation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMeasuringStation'
CREATE INDEX [IX_FK_LocationMeasuringStation]
ON [dbo].[MeasuringStations]
    ([Location_Id]);
GO

-- Creating foreign key on [MeasuringStation_Id] in table 'Measurements'
ALTER TABLE [dbo].[Measurements]
ADD CONSTRAINT [FK_MeasuringStationMeasurement]
    FOREIGN KEY ([MeasuringStation_Id])
    REFERENCES [dbo].[MeasuringStations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MeasuringStationMeasurement'
CREATE INDEX [IX_FK_MeasuringStationMeasurement]
ON [dbo].[Measurements]
    ([MeasuringStation_Id]);
GO

insert into Locations(Address) values('Klisa');
insert into Locations(Address) values('Centar');
insert into Locations(Address) values('Liman');
insert into MeasuringStations(Location_Id,Name) values(1,'kli');
insert into MeasuringStations(Location_Id,Name) values(1,'kli2');
insert into MeasuringStations(Location_Id,Name) values(2,'CentarStanica');
insert into MeasuringStations(Location_Id,Name) values(3,'limanprvi');
-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------