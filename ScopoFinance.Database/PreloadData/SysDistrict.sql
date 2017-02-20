SET IDENTITY_INSERT [dbo].[SysDistrict] ON;

MERGE [dbo].[SysDistrict] AS TARGET
USING
(
			SELECT @District_DHAKA			, N'DHAKA'		, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_GAZIPUR		, N'GAZIPUR'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_NARAYANGONJ	, N'NARAYANGONJ', @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_NARSINGDI		, N'NARSINGDI'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_KISHOREGANJ	, N'KISHOREGANJ', @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_TANGAIL		, N'TANGAIL'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_FARIDPUR		, N'FARIDPUR'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_RAJBARI		, N'RAJBARI'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_MUNSHIGANJ		, N'MUNSHIGANJ'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_GOPALGANJ		, N'GOPALGANJ'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_MADARIPUR		, N'MADARIPUR'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_MANIKGANJ		, N'MANIKGANJ'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()
   UNION	SELECT @District_SHARIATPUR		, N'SHARIATPUR'	, @Division_DHAKA, @User_HOUser, GETDATE(), GETDATE()

) AS SOURCE ([Id],[Name],[DivisionId],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[DivisionId] = SOURCE.[DivisionId],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[DivisionId],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[DivisionId],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [dbo].[SysDistrict] OFF;