CREATE TABLE [dbo].[Organization]
(
	[Id] INT NOT NULL IDENTITY, 
	[BranchId] INT NOT NULL, 
	[OrganizationNo] NVARCHAR(4) NOT NULL,
	[OrganizationName] NVARCHAR(50) NOT NULL,
	[OrgCategoryId] INT NOT NULL, 
    [GenderId] INT NOT NULL, 
    [SetupDate] DATETIME NOT NULL, 
    [MeetingFrequency] INT NOT NULL, 
    [MeetingDate] DATETIME NOT NULL, 
    [VillageId] INT NULL, 
	[IsActive] BIT NOT NULL DEFAULT 1, 
	[UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Org_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id]),
	CONSTRAINT [FK_Org_OrgCategory] FOREIGN KEY ([OrgCategoryId]) REFERENCES [OrgCategory]([Id]),
	CONSTRAINT [FK_Org_Gender] FOREIGN KEY ([GenderId]) REFERENCES [SysGender]([Id]),
	CONSTRAINT [FK_Org_Colc] FOREIGN KEY ([MeetingFrequency]) REFERENCES [SysColcOption]([Id]),
	CONSTRAINT [FK_Org_Village] FOREIGN KEY ([VillageId]) REFERENCES [SysVillage]([Id])
)
