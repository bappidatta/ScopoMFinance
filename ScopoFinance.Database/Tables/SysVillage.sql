CREATE TABLE [lnsav].[SysVillage]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[UnionId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Village] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Village_Union] FOREIGN KEY ([UnionId]) REFERENCES [lnsav].[SysUnion]([Id])
)
