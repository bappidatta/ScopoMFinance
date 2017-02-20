CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL IDENTITY, 
    [ProductCode] NVARCHAR(50) NOT NULL, 
	[ProductName] NVARCHAR(50) NOT NULL, 
    [InterestRate] DECIMAL NOT NULL,
	[ProductTypeId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Product_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [ProductType]([Id]),
	CONSTRAINT [UQ_ProductCode] UNIQUE ([ProductCode])
)
