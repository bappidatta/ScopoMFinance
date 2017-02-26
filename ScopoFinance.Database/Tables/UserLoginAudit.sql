CREATE TABLE [dbo].[UserLoginAudit]
(
	[Id] INT NOT NULL IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [BranchId] INT NOT NULL, 
    [LoggedInTime] DATETIME NOT NULL, 
    [LoggedOutTime] DATETIME NULL,
	CONSTRAINT [PK_UserLoginAudit] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_UserLogin_Branch] FOREIGN KEY ([BranchId]) REFERENCES [lnsav].[Branch]([Id]),
	CONSTRAINT [FK_UserLogin_User] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId])
)
