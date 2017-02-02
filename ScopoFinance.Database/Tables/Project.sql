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
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	[UpdatedBy] NVARCHAR(256) NULL, 
    [UpdatedOn] DATETIME NULL,
	CONSTRAINT [PK_Project] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Project_ProjectType] FOREIGN KEY ([ProjectTypeId]) REFERENCES [ProjectType]([Id]),
	CONSTRAINT [FK_Project_Donor] FOREIGN KEY ([DonorId]) REFERENCES [SysDonor]([Id]),
)
