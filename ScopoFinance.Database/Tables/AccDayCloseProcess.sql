CREATE TABLE [dbo].[AccDayCloseProcess]
(
	[Id] INT NOT NULL IDENTITY,
	[ProcessName] NVARCHAR(255) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	[UpdatedBy] NVARCHAR(256) NULL, 
    [UpdatedOn] DATETIME NULL,
	CONSTRAINT [PK_AccDayCloseProcess] PRIMARY KEY ([Id])
)
