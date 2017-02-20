﻿CREATE TABLE [dbo].[AccDayCloseProcess]
(
	[Id] INT NOT NULL IDENTITY,
	[ProcessName] NVARCHAR(255) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_AccDayCloseProcess] PRIMARY KEY ([Id])
)
