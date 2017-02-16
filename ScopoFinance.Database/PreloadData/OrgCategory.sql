SET IDENTITY_INSERT [dbo].[OrgCategory] ON;

MERGE [dbo].[OrgCategory] AS TARGET
USING
(
			SELECT 1, N'01', N'Center', @User_HOUser, GetDate()
   UNION	SELECT 2, N'02', N'ME', @User_HOUser, GetDate()

) AS SOURCE ([Id],[CategoryCode],[CategoryName],[CreatedBy],[CreatedOn])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[CategoryCode] = SOURCE.[CategoryCode],
		TARGET.[CategoryName] = SOURCE.[CategoryName],
		TARGET.[CreatedBy] = SOURCE.[CreatedBy],
		TARGET.[CreatedOn] = SOURCE.[CreatedOn]
WHEN NOT MATCHED THEN
	INSERT([Id],[CategoryCode],[CategoryName],[CreatedBy],[CreatedOn])
	VALUES(SOURCE.[Id],SOURCE.[CategoryCode],SOURCE.[CategoryName],SOURCE.[CreatedBy],SOURCE.[CreatedOn])
;

SET IDENTITY_INSERT [dbo].[OrgCategory] OFF;