SET IDENTITY_INSERT [lnsav].[SysVillage] ON;

MERGE [lnsav].[SysVillage] AS TARGET
USING
(
			SELECT 1, N'Village 1'	, @Union_KUSHUMHATI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT 2, N'Village 2'	, @Union_KUSHUMHATI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT 3, N'Village 3'	, @Union_KUSHUMHATI, @User_HOUser, GETDATE(), GETDATE()
) AS SOURCE ([Id],[Name],[UnionId],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[UnionId] = SOURCE.[UnionId],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[UnionId],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[UnionId],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [lnsav].[SysVillage] OFF;