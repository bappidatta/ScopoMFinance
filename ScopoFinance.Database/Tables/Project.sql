CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL IDENTITY, 
    [ProjectCode] NVARCHAR(50) NOT NULL, 
    [ProjectName] NVARCHAR(50) NOT NULL, 
    [ProjectDuration] DateTime NULL,
	[ProjectTypeId] INT NOT NULL,
	[DonorId] INT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Project] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Project_ProjectType] FOREIGN KEY ([ProjectTypeId]) REFERENCES [ProjectType]([Id]),
	CONSTRAINT [FK_Project_Donor] FOREIGN KEY ([DonorId]) REFERENCES [SysDonor]([Id]),
	CONSTRAINT [UQ_ProjectCode] UNIQUE ([ProjectCode])
)
