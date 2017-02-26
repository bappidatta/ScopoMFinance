SET IDENTITY_INSERT [lnsav].[SysUnion] ON;

MERGE [lnsav].[SysUnion] AS TARGET
USING
(
			SELECT @Union_KUSHUMHATI	, N'KUSHUMHATI'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_MAHMUDPUR		, N'MAHMUDPUR'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_MOKSEDPUR		, N'MOKSEDPUR'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_NARISHA		, N'NARISHA'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_NAYABARI		, N'NAYABARI'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_RAIPARA		, N'RAIPARA'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_SUTARPARA		, N'SUTARPARA'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Union_BILASHPUR		, N'BILASHPUR'	, @Upazila_DHAMRAI, @User_HOUser, GETDATE(), GETDATE()
) AS SOURCE ([Id],[Name],[UpazilaId],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[UpazilaId] = SOURCE.[UpazilaId],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[UpazilaId],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[UpazilaId],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [lnsav].[SysUnion] OFF;