CREATE DATABASE DocumentWorker
GO

USE DocumentWorker
GO

CREATE TABLE [dbo].[WordInfos](
    [Id] int IDENTITY(1, 1) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Count] int NOT NULL,
	PRIMARY KEY(Id)
)
GO

CREATE TABLE [dbo].[WordInfoTemps](
    [Id] int IDENTITY(1, 1) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Count] int NOT NULL,
	[IsNew] bit NOT NULL DEFAULT 1,
	PRIMARY KEY(Id)
)
GO