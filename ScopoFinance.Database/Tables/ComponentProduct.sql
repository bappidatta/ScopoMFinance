CREATE TABLE [lnsav].[ComponentProduct]
(
	[ComponentId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	CONSTRAINT [PK_ComponentProduct] PRIMARY KEY ([ComponentId], [ProductId]),
	CONSTRAINT [FK_ComponentProduct_Component] FOREIGN KEY ([ComponentId]) REFERENCES [lnsav].[Component]([Id]),
	CONSTRAINT [FK_ComponentProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [lnsav].[Product]([Id])
)
