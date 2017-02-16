CREATE TABLE [dbo].[ProjectWiseProductMapping]
(
	[Id] INT NOT NULL IDENTITY,
	[ProjectId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	CONSTRAINT [PK_ProjectWiseProductMapping] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ProjectProduct_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]),
	CONSTRAINT [FK_ProjectProduct_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
