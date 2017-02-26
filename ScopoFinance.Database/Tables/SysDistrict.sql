CREATE TABLE [lnsav].[SysDistrict]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[DivisionId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_District] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_District_Division] FOREIGN KEY ([DivisionId]) REFERENCES [lnsav].[SysDivision]([Id])
)
