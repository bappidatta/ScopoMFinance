CREATE TABLE [dbo].[UserProfile]
(
	[UserId] NVARCHAR (128) NOT NULL, 
    [FirstName] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [MobileNo] NVARCHAR(14) NOT NULL, 
    [BranchId] INT NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [UQ_UserProfileMobileNo] UNIQUE ([MobileNo]), 
	CONSTRAINT [FK_UserProfile_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id]),
	CONSTRAINT [FK_UserProfile_AspNetUser] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
