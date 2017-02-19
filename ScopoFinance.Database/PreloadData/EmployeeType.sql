SET IDENTITY_INSERT [dbo].[EmployeeType] ON;

MERGE [dbo].[EmployeeType] AS TARGET
USING
(
			SELECT 1, N'IT Manager', @User_HOUser, GetDate()
   UNION	SELECT 2, N'HR Manager', @User_HOUser, GetDate()

) AS SOURCE ([Id],[Name],[CreatedBy],[CreatedOn])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[CreatedBy] = SOURCE.[CreatedBy],
		TARGET.[CreatedOn] = SOURCE.[CreatedOn]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[CreatedBy],[CreatedOn])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[CreatedBy],SOURCE.[CreatedOn])
;

SET IDENTITY_INSERT [dbo].[EmployeeType] OFF;