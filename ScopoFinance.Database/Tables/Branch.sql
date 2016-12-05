CREATE TABLE [dbo].[Branch]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(255) NOT NULL, 
    [OpenDate] DATETIME NOT NULL, 
    [Status] BIT NOT NULL,
	[IsHeadOffice] BIT NOT NULL, 
    CONSTRAINT [PK_Branch] PRIMARY KEY ([Id])
)
