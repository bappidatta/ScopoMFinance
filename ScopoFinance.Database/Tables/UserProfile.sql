CREATE TABLE [dbo].[UserProfile]
(
	[UserId] NVARCHAR (128) NOT NULL, 
    [FirstName] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [MobileNo] NVARCHAR(14) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [ResUserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [UQ_UserProfileMobileNo] UNIQUE ([MobileNo]), 
	CONSTRAINT [FK_UserProfile_AspNetUser] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
