CREATE TABLE [lnsav].[Product]
(
	[Id] INT NOT NULL IDENTITY, 
    [ProductCode] NVARCHAR(50) NOT NULL, 
	[ProductName] NVARCHAR(50) NOT NULL, 
    [InterestRate] DECIMAL NOT NULL,
	[ProductTypeId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_LoanProduct] PRIMARY KEY ([Id]),
	CONSTRAINT [UQ_LoanProductCode] UNIQUE ([ProductCode]),
	CONSTRAINT [FK_Product_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [lnsav].[ProductType]([Id]),
)
