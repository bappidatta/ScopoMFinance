CREATE TABLE [dbo].[BranchWiseProjectMapping]
(
	[BranchId] INT NOT NULL,
	[ProjectId] INT NOT NULL,
	CONSTRAINT [PK_BranchWiseProjectMapping] PRIMARY KEY ([BranchId], [ProjectId]),
	CONSTRAINT [FK_BranchProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]),
	CONSTRAINT [FK_BranchProject_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id])
)
