SET IDENTITY_INSERT [dbo].[Branch] ON;

MERGE [dbo].[Branch] AS TARGET
USING
(
			SELECT @Branch_HO	, N'Head Office', GetDate(), 1, 1
   UNION	SELECT @Branch_101	, N'Branch 101'	, GetDate(), 1, 0
   UNION	SELECT @Branch_102	, N'Branch 102'	, GetDate(), 1, 0
   UNION	SELECT @Branch_103	, N'Branch 103'	, GetDate(), 1, 0
   UNION	SELECT @Branch_104	, N'Branch 104'	, GetDate(), 1, 0
   UNION	SELECT @Branch_105	, N'Branch 105'	, GetDate(), 1, 0
   UNION	SELECT @Branch_106	, N'Branch 106'	, GetDate(), 1, 0
   UNION	SELECT @Branch_107	, N'Branch 107'	, GetDate(), 1, 0
   UNION	SELECT @Branch_108	, N'Branch 108'	, GetDate(), 1, 0
   UNION	SELECT @Branch_109	, N'Branch 109'	, GetDate(), 1, 0
   UNION	SELECT @Branch_110	, N'Branch 110'	, GetDate(), 1, 0
   UNION	SELECT @Branch_111	, N'Branch 111'	, GetDate(), 1, 0
   UNION	SELECT @Branch_112	, N'Branch 112'	, GetDate(), 1, 0
   UNION	SELECT @Branch_113	, N'Branch 113'	, GetDate(), 1, 0
   UNION	SELECT @Branch_114	, N'Branch 114'	, GetDate(), 1, 0
   UNION	SELECT @Branch_115	, N'Branch 115'	, GetDate(), 1, 0
   UNION	SELECT @Branch_116	, N'Branch 116'	, GetDate(), 1, 0
   UNION	SELECT @Branch_117	, N'Branch 117'	, GetDate(), 1, 0
   UNION	SELECT @Branch_118	, N'Branch 118'	, GetDate(), 1, 0

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