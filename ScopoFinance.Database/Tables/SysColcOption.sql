﻿CREATE TABLE [dbo].[SysColcOption]
(
	[Id] INT NOT NULL IDENTITY, 
	[Name] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_CollectionOption] PRIMARY KEY ([Id])
)
