CREATE TABLE [dbo].[AccDayOpenClose]
(
	[Id] INT NOT NULL IDENTITY,
	[BranchId] INT NOT NULL,
	[CurrentDate] DATETIME NOT NULL,
	[CloseRequest] BIT NOT NULL DEFAULT 0,
	[IsClosed] BIT NOT NULL DEFAULT 0,
	[CloseRequestBy] INT NULL,
	[CloseRequestAt] DATETIME NULL,
	[OpenedAt] DATETIME NOT NULL,
	[ClosedAt] DATETIME NULL,
	CONSTRAINT [PK_DayOpenClose] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_DayOpenClose_Branch] FOREIGN KEY ([BranchId]) REFERENCES [Branch]([Id])
)