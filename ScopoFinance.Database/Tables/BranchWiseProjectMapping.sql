CREATE TABLE [dbo].[BranchWiseProjectMapping]
(
	[Id] INT NOT NULL IDENTITY, 
	[BranchId] INT NOT NULL,
	[ProjectId] INT NOT NULL,
	CONSTRAINT [PK_BranchWiseProjectMapping] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_BranchProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]),
	CONSTRAINT [FK_BranchProject_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id])
)
