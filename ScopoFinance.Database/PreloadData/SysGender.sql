SET IDENTITY_INSERT [lnsav].[SysGender] ON;

MERGE [lnsav].[SysGender] AS TARGET
USING
(
			SELECT @Gender_Male		, N'Male'  , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Gender_Female	, N'Female', @User_HOUser, GETDATE(), GETDATE()

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

SET IDENTITY_INSERT [lnsav].[SysGender] OFF;