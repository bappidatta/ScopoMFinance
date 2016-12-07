SET IDENTITY_INSERT [dbo].[SysUpazila] ON;

MERGE [dbo].[SysUpazila] AS TARGET
USING
(
			SELECT @Upazila_DHAMRAI		, N'DHAMRAI'	, @District_DHAKA
   UNION	SELECT @Upazila_DOHAR		, N'DOHAR'		, @District_DHAKA
   UNION	SELECT @Upazila_KERANIGANJ	, N'KERANIGANJ'	, @District_DHAKA
   UNION	SELECT @Upazila_NAWABGANJ	, N'NAWABGANJ'	, @District_DHAKA
   UNION	SELECT @Upazila_SAVAR		, N'SAVAR'		, @District_DHAKA
) AS SOURCE ([Id],[Name],[DistrictId])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[DistrictId] = SOURCE.[DistrictId]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[DistrictId])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[DistrictId])
;

SET IDENTITY_INSERT [dbo].[SysUpazila] OFF;