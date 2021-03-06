﻿CREATE TABLE [lnsav].[Employee]
(
	[Id] INT NOT NULL IDENTITY,
	[BranchId] INT NOT NULL, 
	[EmployeeNo] NVARCHAR(10) NOT NULL,
	[EmployeeName] NVARCHAR(255) NOT NULL,
	[IsCreditOfficer] BIT NOT NULL DEFAULT 0,
	[JoiningDate] DATETIME NOT NULL,
	[ResignDate] DATETIME NULL, 
    [GenderId] INT NOT NULL, 
    [EmployeeTypeId] INT NOT NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [PhoneNo] NVARCHAR(20) NULL, 
	[Remarks] NVARCHAR(MAX) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Employee] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY ([EmployeeTypeId]) REFERENCES [lnsav].[EmployeeType]([Id]),
	CONSTRAINT [FK_Employee_Gender] FOREIGN KEY ([GenderId]) REFERENCES [lnsav].[SysGender]([Id]),
	CONSTRAINT [FK_Employee_Branch] FOREIGN KEY ([BranchId]) REFERENCES [lnsav].[Branch]([Id]),
	CONSTRAINT [UQ_EmployeeNo] UNIQUE ([EmployeeNo])
)
