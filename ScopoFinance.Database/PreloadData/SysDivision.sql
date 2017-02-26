SET IDENTITY_INSERT [lnsav].[SysDivision] ON;

MERGE [lnsav].[SysDivision] AS TARGET
USING
(
			SELECT @Division_DHAKA		, N'DHAKA'	   , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Division_CHITTAGONG , N'CHITTAGONG', @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_RAJSHAHI	, N'RAJSHAHI'  , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_SYLHET		, N'SYLHET'	   , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_BARISAL		, N'BARISAL'   , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_KHULNA		, N'KHULNA'	   , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_RANGPUR		, N'RANGPUR'   , @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Divsion_MYMENSINGH	, N'MYMENSINGH', @User_HOUser, GETDATE(), GETDATE()

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

SET IDENTITY_INSERT [lnsav].[SysDivision] OFF;