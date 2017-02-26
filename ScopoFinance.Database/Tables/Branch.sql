﻿CREATE TABLE [lnsav].[Branch]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(255) NOT NULL, 
    [OpenDate] DATETIME NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	[IsHeadOffice] BIT NOT NULL, 
    CONSTRAINT [PK_Branch] PRIMARY KEY ([Id])
)
