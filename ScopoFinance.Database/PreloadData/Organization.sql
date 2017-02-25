SET IDENTITY_INSERT [dbo].[Organization] ON;

MERGE [dbo].[Organization] AS TARGET
USING
(
			SELECT 1, @Branch_101, N'1001', N'Active Center 1 - Br 1'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 2, @Branch_101, N'1002', N'Active Center 1 - Br 1'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 3, @Branch_101, N'1003', N'Active Center 3 - Br 1'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 4, @Branch_101, N'1003', N'Inactive Center 1 - Br 1'	, 1, 1, GETDATE(), 1, GETDATE(), 0, @User_BranchUser_101, GetDate(), GetDate()

   UNION	SELECT 5, @Branch_102, N'1001', N'Active Center 1 - Br 2'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 6, @Branch_102, N'1002', N'Active Center 1 - Br 2'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 7, @Branch_102, N'1003', N'Active Center 3 - Br 2'	, 1, 1, GETDATE(), 1, GETDATE(), 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 8, @Branch_102, N'1003', N'Inactive Center 1 - Br 2'	, 1, 1, GETDATE(), 1, GETDATE(), 0, @User_BranchUser_102, GetDate(), GetDate()

) AS SOURCE ([Id],[BranchId],[OrganizationNo],[OrganizationName],[OrgCategoryId],[GenderId],[SetupDate],[MeetingFrequency],[MeetingDate],[IsActive],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[BranchId]			= SOURCE.[BranchId],
		TARGET.[OrganizationNo]		= SOURCE.[OrganizationNo],
		TARGET.[OrganizationName]	= SOURCE.[OrganizationName],
		TARGET.[OrgCategoryId]		= SOURCE.[OrgCategoryId],
		TARGET.[GenderId]			= SOURCE.[GenderId],
		TARGET.[SetupDate]			= SOURCE.[SetupDate],
		TARGET.[MeetingFrequency]	= SOURCE.[MeetingFrequency],
		TARGET.[MeetingDate]		= SOURCE.[MeetingDate],
		TARGET.[IsActive]			= SOURCE.[IsActive],
		TARGET.[UserId]				= SOURCE.[UserId],
		TARGET.[SystemDate]			= SOURCE.[SystemDate],
		TARGET.[SetDate]			= SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[BranchId],[OrganizationNo],[OrganizationName],[OrgCategoryId],[GenderId],[SetupDate],[MeetingFrequency],[MeetingDate],[IsActive],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[BranchId],SOURCE.[OrganizationNo],SOURCE.[OrganizationName],SOURCE.[OrgCategoryId],SOURCE.[GenderId],SOURCE.[SetupDate],SOURCE.[MeetingFrequency],SOURCE.[MeetingDate],SOURCE.[IsActive],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [dbo].[Organization] OFF;