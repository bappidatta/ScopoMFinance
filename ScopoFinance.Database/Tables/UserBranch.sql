CREATE TABLE [dbo].[UserBranch]
(
	[Id] INT NOT NULL IDENTITY,
    [UserId] NVARCHAR (128) NOT NULL, 
    [BranchId] INT NOT NULL
	CONSTRAINT [PK_UserBranch] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_UserBranch_User] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]),
	CONSTRAINT [FK_UserBranch_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id])
)
