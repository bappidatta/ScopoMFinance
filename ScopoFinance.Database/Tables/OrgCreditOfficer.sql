CREATE TABLE [lnsav].[OrgCreditOfficer]
(
	[BranchId] INT NOT NULL,
	[EmployeeId] INT NOT NULL,
	[OrganizationId] INT NOT NULL,
	[AssignedDate] DATETIME NULL,
	[ReleaseDate] DATETIME NULL,
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_OrgCreditOfficer] PRIMARY KEY ([BranchId], [EmployeeId], [OrganizationId]),
	CONSTRAINT [FK_OrgCO_Branch] FOREIGN KEY ([BranchId]) REFERENCES [lnsav].[Branch]([Id]),
	CONSTRAINT [FK_OrgCO_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [lnsav].[Employee]([Id]),
	CONSTRAINT [FK_OrgCO_Org] FOREIGN KEY ([OrganizationId]) REFERENCES [lnsav].[Organization]([Id])
)
