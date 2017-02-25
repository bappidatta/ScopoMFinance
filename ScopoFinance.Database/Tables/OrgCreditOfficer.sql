CREATE TABLE [dbo].[OrgCreditOfficer]
(
	[BranchId] INT NOT NULL,
	[EmployeeId] INT NOT NULL,
	[OrganizationId] INT NOT NULL,
	[AssignedDate] DATETIME NULL,
	[ReleaseDate] DATETIME NULL,
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [FK_OrgCO_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id]),
	CONSTRAINT [FK_OrgCO_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]),
	CONSTRAINT [FK_OrgCO_Org] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
