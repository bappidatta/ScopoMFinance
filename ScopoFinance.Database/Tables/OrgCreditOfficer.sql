CREATE TABLE [dbo].[OrgCreditOfficer]
(
	[Id] INT NOT NULL IDENTITY,
	[BranchId] INT NOT NULL,
	[EmployeeId] INT NOT NULL,
	[OrganizationId] INT NOT NULL,
	[AssignedDate] DATETIME NOT NULL,
	[ReleaseDate] DATETIME NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	[UpdatedBy] NVARCHAR(256) NULL, 
    [UpdatedOn] DATETIME NULL,
	CONSTRAINT [PK_OrgCreditOfficer] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_OrgCO_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id]),
	CONSTRAINT [FK_OrgCO_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]),
	CONSTRAINT [FK_OrgCO_Org] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
