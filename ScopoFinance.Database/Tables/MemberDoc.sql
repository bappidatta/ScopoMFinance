CREATE TABLE [lnsav].[MemberDoc]
(
	[Id] INT NOT NULL IDENTITY,
	[MemberId] INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL, 
    [ContentType] NVARCHAR(50) NOT NULL, 
    [Content] VARBINARY(MAX) NOT NULL,
	[DocTypeId] INT NOT NULL, 
    CONSTRAINT [PK_MemberDoc] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_MemberDoc_Member] FOREIGN KEY ([MemberId]) REFERENCES [lnsav].[Member]([Id]),
	CONSTRAINT [FK_MemberDoc_DocType] FOREIGN KEY ([DocTypeId]) REFERENCES [lnsav].[SysDocType]([Id])
)
