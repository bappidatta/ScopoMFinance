CREATE TABLE [lnsav].[ComponentSavingsProduct]
(
	[ComponentId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	CONSTRAINT [PK_ComponentSavingsProduct] PRIMARY KEY ([ComponentId], [ProductId]),
	CONSTRAINT [FK_ComponentSavingsProduct_Component] FOREIGN KEY ([ComponentId]) REFERENCES [lnsav].[Component]([Id]),
	CONSTRAINT [FK_ComponentSavingsProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [lnsav].[SavingsProduct]([Id])
)
