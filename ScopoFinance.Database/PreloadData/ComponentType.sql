SET IDENTITY_INSERT [dbo].[ComponentType] ON;

MERGE [dbo].[ComponentType] AS TARGET
USING
(
			SELECT 1, N'Core Component', @User_HOUser, GetDate(), GetDate()
   UNION	SELECT 2, N'Non Core Component', @User_HOUser, GetDate(), GetDate()

) AS SOURCE ([Id],[Name],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [dbo].[ComponentType] OFF;