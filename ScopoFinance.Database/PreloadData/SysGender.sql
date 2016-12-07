SET IDENTITY_INSERT [dbo].[SysGender] ON;

MERGE [dbo].[SysGender] AS TARGET
USING
(
			SELECT @Gender_Male		, N'Male'
   UNION	SELECT @Gender_Female	, N'Female'

) AS SOURCE ([Id],[Name])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name])
	VALUES(SOURCE.[Id],SOURCE.[Name])
;

SET IDENTITY_INSERT [dbo].[SysGender] OFF;