﻿CREATE TABLE [dbo].[SysUnion]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[UpazilaId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Union] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Union_Upazila] FOREIGN KEY ([UpazilaId]) REFERENCES [SysUpazila]([Id])
)
