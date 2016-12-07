﻿CREATE TABLE [dbo].[SysUnion]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[UpazilaId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL DEFAULT 'ScopoMFinance', 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_Union] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Union_Upazila] FOREIGN KEY ([UpazilaId]) REFERENCES [SysUpazila]([Id])
)
