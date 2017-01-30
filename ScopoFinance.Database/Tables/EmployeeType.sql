﻿CREATE TABLE [dbo].[EmployeeType]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(256) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	[UpdatedBy] NVARCHAR(256) NULL, 
    [UpdatedOn] DATETIME NULL,
	CONSTRAINT [PK_EmployeeType] PRIMARY KEY ([Id])
)
