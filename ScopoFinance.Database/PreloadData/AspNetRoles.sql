MERGE [dbo].[AspNetRoles] AS TARGET
USING
(
			SELECT @Role_HOUser		, 'HOUser'
	UNION	SELECT @Role_BranchUser	, 'BranchUser'

) AS SOURCE ([Id], [Name])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED THEN
	INSERT([Id], [Name])
	VALUES(SOURCE.[Id], SOURCE.[Name])
;