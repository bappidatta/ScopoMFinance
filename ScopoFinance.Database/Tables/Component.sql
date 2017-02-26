CREATE TABLE [lnsav].[Component]
(
	[Id] INT NOT NULL IDENTITY, 
    [ComponentCode] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Duration] DateTime NULL,
	[ComponentTypeId] INT NOT NULL,
	[DonorId] INT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[SystemDate] DATETIME NOT NULL,
    [SetDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Component] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Component_ComponentType] FOREIGN KEY ([ComponentTypeId]) REFERENCES [lnsav].[ComponentType]([Id]),
	CONSTRAINT [FK_Component_Donor] FOREIGN KEY ([DonorId]) REFERENCES [lnsav].[SysDonor]([Id]),
	CONSTRAINT [UQ_ComponentCode] UNIQUE ([ComponentCode])
)
