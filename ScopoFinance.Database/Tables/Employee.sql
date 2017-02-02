CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL IDENTITY,
	[EmployeeNo] NVARCHAR(10) NOT NULL,
	[EmployeeName] NVARCHAR(255) NOT NULL,
	[JoiningDate] DATETIME NOT NULL,
	[ResignDate] DATETIME NULL, 
    [GenderId] INT NOT NULL, 
    [EmployeeTypeId] INT NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [PhoneNo] NVARCHAR(20) NOT NULL, 
	[Remarks] NVARCHAR(MAX) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] NVARCHAR(256) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL,
	[UpdatedBy] NVARCHAR(256) NULL, 
    [UpdatedOn] DATETIME NULL,
)
