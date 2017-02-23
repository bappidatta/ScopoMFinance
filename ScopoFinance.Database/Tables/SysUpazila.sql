CREATE TABLE [dbo].[SysUpazila]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL, 
	[DistrictId] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Upazila] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Upazila_District] FOREIGN KEY ([DistrictId]) REFERENCES [SysDistrict]([Id])
)
