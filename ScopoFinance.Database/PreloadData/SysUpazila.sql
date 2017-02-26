SET IDENTITY_INSERT [lnsav].[SysUpazila] ON;

MERGE [lnsav].[SysUpazila] AS TARGET
USING
(
			SELECT @Upazila_DHAMRAI		, N'DHAMRAI'	, @District_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Upazila_DOHAR		, N'DOHAR'		, @District_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Upazila_KERANIGANJ	, N'KERANIGANJ'	, @District_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Upazila_NAWABGANJ	, N'NAWABGANJ'	, @District_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @Upazila_SAVAR		, N'SAVAR'		, @District_DHAKA, @User_HOUser, GETDATE(), GETDATE()
) AS SOURCE ([Id],[Name],[DistrictId],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[DistrictId] = SOURCE.[DistrictId],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[DistrictId],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[DistrictId],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [lnsav].[SysUpazila] OFF;