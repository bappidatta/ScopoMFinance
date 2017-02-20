CREATE TABLE [dbo].[SysDistrict]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[DivisionId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_District] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_District_Division] FOREIGN KEY ([DivisionId]) REFERENCES [SysDivision]([Id])
)
