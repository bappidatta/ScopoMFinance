CREATE TABLE [lnsav].[BranchComponent]
(
	[BranchId] INT NOT NULL,
	[ComponentId] INT NOT NULL,
	CONSTRAINT [PK_BranchComponent] PRIMARY KEY ([BranchId], [ComponentId]),
	CONSTRAINT [FK_BranchComponent_Component] FOREIGN KEY ([ComponentId]) REFERENCES [lnsav].[Component]([Id]),
	CONSTRAINT [FK_BranchComponent_Branch] FOREIGN KEY ([BranchId]) REFERENCES [lnsav].[Branch]([Id])
)
