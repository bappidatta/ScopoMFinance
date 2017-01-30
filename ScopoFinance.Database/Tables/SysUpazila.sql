CREATE TABLE [dbo].[SysUpazila]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[DistrictId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL DEFAULT 'ScopoMFinance', 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_Upazila] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Upazila_District] FOREIGN KEY ([DistrictId]) REFERENCES [SysDistrict]([Id])
)
