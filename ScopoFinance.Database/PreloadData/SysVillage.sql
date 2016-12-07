SET IDENTITY_INSERT [dbo].[SysVillage] ON;

MERGE [dbo].[SysVillage] AS TARGET
USING
(
			SELECT 1, N'Village 1'	, @Union_KUSHUMHATI
   UNION	SELECT 2, N'Village 2'	, @Union_KUSHUMHATI
   UNION	SELECT 3, N'Village 3'	, @Union_KUSHUMHATI
) AS SOURCE ([Id],[Name],[UnionId])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[UnionId] = SOURCE.[UnionId]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[UnionId])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[UnionId])
;

SET IDENTITY_INSERT [dbo].[SysVillage] OFF;