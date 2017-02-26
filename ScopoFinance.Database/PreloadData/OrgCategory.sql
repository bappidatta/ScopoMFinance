SET IDENTITY_INSERT [lnsav].[OrgCategory] ON;

MERGE [lnsav].[OrgCategory] AS TARGET
USING
(
			SELECT 1, N'01', N'Center', @User_HOUser, GetDate(), GetDate()
   UNION	SELECT 2, N'02', N'ME', @User_HOUser, GetDate(), GetDate()

) AS SOURCE ([Id],[CategoryCode],[CategoryName],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[CategoryCode] = SOURCE.[CategoryCode],
		TARGET.[CategoryName] = SOURCE.[CategoryName],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[CategoryCode],[CategoryName],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[CategoryCode],SOURCE.[CategoryName],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [lnsav].[OrgCategory] OFF;