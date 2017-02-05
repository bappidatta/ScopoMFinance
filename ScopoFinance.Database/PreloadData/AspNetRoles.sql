MERGE [dbo].[AspNetRoles] AS TARGET
USING
(
			SELECT @Role_HOUser		, 'superuser'
	UNION	SELECT @Role_BranchUser	, 'branchuser'
	UNION	SELECT @Role_BranchManager	, 'branchmanager'
	UNION	SELECT @Role_AreaCoordinator	, 'areacoordinator'
) AS SOURCE ([Id], [Name])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED THEN
	INSERT([Id], [Name])
	VALUES(SOURCE.[Id], SOURCE.[Name])
;