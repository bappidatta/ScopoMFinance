SET IDENTITY_INSERT [dbo].[Branch] ON;

MERGE [dbo].[Branch] AS TARGET
USING
(
			SELECT @Branch_HO	, N'Head Office', GetDate(), 1, 1
   UNION	SELECT @Branch_101	, N'Branch 101'	, GetDate(), 1, 0
   UNION	SELECT @Branch_102	, N'Branch 102'	, GetDate(), 1, 0

) AS SOURCE ([Id],[Name],[OpenDate],[Status],[IsHeadOffice])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[OpenDate] = SOURCE.[OpenDate],
		TARGET.[Status] = SOURCE.[Status],
		TARGET.[IsHeadOffice] = SOURCE.[IsHeadOffice]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[OpenDate],[Status],[IsHeadOffice])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[OpenDate],SOURCE.[Status],SOURCE.[IsHeadOffice])
;

SET IDENTITY_INSERT [dbo].[Branch] OFF;