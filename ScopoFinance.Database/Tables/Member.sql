﻿CREATE TABLE [lnsav].[Member]
(
	[Id] INT NOT NULL IDENTITY,
	[BranchId] INT NOT NULL, 
    [OrganizationId] INT NOT NULL, 
    [ComponentId] INT NOT NULL, 
    [MemberNo] NVARCHAR(6) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
	[GenderId] INT NOT NULL,
	[JoiningDate] DATETIME NOT NULL, 
	[SavingsTarget] Decimal NOT NULL,
	[DateOfBirth] DATETIME NULL,
	[NID] NVARCHAR(17) NULL,
	[MobileNo] NVARCHAR(14) NULL,
	[Address] NVARCHAR(MAX) NULL,
	[Email] NVARCHAR(50) NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Member_Branch] FOREIGN KEY ([BranchId]) REFERENCES [lnsav].[Branch]([Id]),
	CONSTRAINT [FK_Member_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [lnsav].[Organization]([Id]),
	CONSTRAINT [FK_Member_Component] FOREIGN KEY ([ComponentId]) REFERENCES [lnsav].[Component]([Id]),
	CONSTRAINT [FK_Member_Gender] FOREIGN KEY ([GenderId]) REFERENCES [lnsav].[SysGender]([Id])
)
