CREATE TABLE [dbo].[ComponentLoanProduct]
(
	[ComponentId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	CONSTRAINT [PK_ComponentLoanProduct] PRIMARY KEY ([ComponentId], [ProductId]),
	CONSTRAINT [FK_ComponentLoanProduct_Component] FOREIGN KEY ([ComponentId]) REFERENCES [Component]([Id]),
	CONSTRAINT [FK_ComponentLoanProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [LoanProduct]([Id])
)
