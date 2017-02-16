CREATE TABLE [dbo].[AccDayCloseProgress]
(
	[Id] INT NOT NULL IDENTITY,
	[DayCloseProcessId] INT NOT NULL,
	[DayOpenCloseId] INT NOT NULL,
	[Status] INT NOT NULL,
	CONSTRAINT [PK_AccDayCloseProgress] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_AccDayCloseProgress_AccDayCloseProcess] FOREIGN KEY ([DayCloseProcessId]) REFERENCES [AccDayCloseProcess]([Id]),
	CONSTRAINT [FK_AccDayCloseProgress_AccDayOpenClose] FOREIGN KEY ([DayOpenCloseId]) REFERENCES [AccDayOpenClose]([Id])
)
