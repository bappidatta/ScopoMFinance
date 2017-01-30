CREATE TABLE [dbo].[SysDistrict]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[DivisionId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL DEFAULT 'ScopoMFinance', 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_District] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_District_Division] FOREIGN KEY ([DivisionId]) REFERENCES [SysDivision]([Id])
)
