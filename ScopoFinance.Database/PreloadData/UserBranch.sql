SET IDENTITY_INSERT [dbo].[UserBranch] ON;

MERGE [dbo].[UserBranch] AS TARGET
USING
(
			SELECT 1	, @User_HOUser, @Branch_HO
   UNION	SELECT 2	, @User_BranchUser_101	, @Branch_101
   UNION	SELECT 3	, @User_BranchUser_102	, @Branch_102
   UNION	SELECT 4	, @User_HOUser	, @Branch_101

) AS SOURCE ([Id],[UserId],[BranchId])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[BranchId] = SOURCE.[BranchId]
WHEN NOT MATCHED THEN
	INSERT([Id],[UserId],[BranchId])
	VALUES(SOURCE.[Id],SOURCE.[UserId],SOURCE.[BranchId])
;

SET IDENTITY_INSERT [dbo].[UserBranch] OFF;