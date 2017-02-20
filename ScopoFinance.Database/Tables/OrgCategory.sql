CREATE TABLE [dbo].[OrgCategory]
(
	[Id] INT NOT NULL IDENTITY, 
    [CategoryCode] NVARCHAR(5) NOT NULL, 
    [CategoryName] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_OrgCategory] PRIMARY KEY ([Id])
)
