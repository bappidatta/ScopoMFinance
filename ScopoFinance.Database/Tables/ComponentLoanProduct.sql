CREATE TABLE [lnsav].[ComponentLoanProduct]
(
	[ComponentId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	CONSTRAINT [PK_ComponentLoanProduct] PRIMARY KEY ([ComponentId], [ProductId]),
	CONSTRAINT [FK_ComponentLoanProduct_Component] FOREIGN KEY ([ComponentId]) REFERENCES [lnsav].[Component]([Id]),
	CONSTRAINT [FK_ComponentLoanProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [lnsav].[LoanProduct]([Id])
)
