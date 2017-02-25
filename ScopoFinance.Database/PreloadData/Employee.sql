SET IDENTITY_INSERT [dbo].[Employee] ON;

MERGE [dbo].[Employee] AS TARGET
USING
(
			SELECT 1, @Branch_101, N'1011001', N'Active CO 1 - Br 1'		, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 2, @Branch_101, N'1011002', N'Active CO 2 - Br 1'		, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 3, @Branch_101, N'1011003', N'Active CO 3 - Br 1'		, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 4, @Branch_101, N'1011004', N'Inactive CO 1 - Br 1'		, 1, GETDATE(), NULL, @Gender_Male, 1, 0, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 5, @Branch_101, N'1011005', N'Active Employee 1 - Br 1'	, 0, GETDATE(), NULL, @Gender_Male, 2, 1, @User_BranchUser_101, GetDate(), GetDate()
   UNION	SELECT 6, @Branch_101, N'1011006', N'Inactive Employee 1 - Br 1', 0, GETDATE(), NULL, @Gender_Male, 2, 0, @User_BranchUser_101, GetDate(), GetDate()

   UNION	SELECT 7 , @Branch_102, N'1021001', N'Active CO 1 - Br 2'			, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 8 , @Branch_102, N'1021002', N'Active CO 2 - Br 2'			, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 9 , @Branch_102, N'1021003', N'Active CO 3 - Br 2'			, 1, GETDATE(), NULL, @Gender_Male, 1, 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 10, @Branch_102, N'1021004', N'Inactive CO 1 - Br 2'			, 1, GETDATE(), NULL, @Gender_Male, 1, 0, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 11, @Branch_102, N'1021005', N'Active Employee 1 - Br 2'		, 0, GETDATE(), NULL, @Gender_Male, 2, 1, @User_BranchUser_102, GetDate(), GetDate()
   UNION	SELECT 12, @Branch_102, N'1021006', N'Inactive Employee 1 - Br 2'	, 0, GETDATE(), NULL, @Gender_Male, 2, 0, @User_BranchUser_102, GetDate(), GetDate()

) AS SOURCE ([Id],[BranchId],[EmployeeNo],[EmployeeName],[IsCreditOfficer],[JoiningDate],[ResignDate],[GenderId],[EmployeeTypeId],[IsActive],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[BranchId]		 = SOURCE.[BranchId],
		TARGET.[EmployeeNo]		 = SOURCE.[EmployeeNo],
		TARGET.[EmployeeName]	 = SOURCE.[EmployeeName],
		TARGET.[IsCreditOfficer] = SOURCE.[IsCreditOfficer],
		TARGET.[JoiningDate]	 = SOURCE.[JoiningDate],
		TARGET.[ResignDate]		 = SOURCE.[ResignDate],
		TARGET.[GenderId]		 = SOURCE.[GenderId],
		TARGET.[EmployeeTypeId]  = SOURCE.[EmployeeTypeId],
		TARGET.[IsActive]		 = SOURCE.[IsActive],
		TARGET.[UserId]			 = SOURCE.[UserId],
		TARGET.[SystemDate]		 = SOURCE.[SystemDate],
		TARGET.[SetDate]		 = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[BranchId],[EmployeeNo],[EmployeeName],[IsCreditOfficer],[JoiningDate],[ResignDate],[GenderId],[EmployeeTypeId],[IsActive],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[BranchId],SOURCE.[EmployeeNo],SOURCE.[EmployeeName],SOURCE.[IsCreditOfficer],SOURCE.[JoiningDate],SOURCE.[ResignDate],SOURCE.[GenderId],SOURCE.[EmployeeTypeId],SOURCE.[IsActive],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [dbo].[Employee] OFF;