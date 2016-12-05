-- ===================================================================================================
--					GENERATE USERS IN ROLES DATA
-- ===================================================================================================

MERGE [dbo].[AspNetUserRoles] AS TARGET
USING
(
          SELECT @User_HOUser			, @Role_HOUser   
	UNION SELECT @User_BranchUser_101	, @Role_BranchUser      
	UNION SELECT @User_BranchUser_102	, @Role_BranchUser      

) AS SOURCE ([UserId], [RoleId])
	ON TARGET.[UserId] = SOURCE.[UserId] AND TARGET.[RoleId] = SOURCE.[RoleId]
WHEN NOT MATCHED THEN
	INSERT([UserId], [RoleId])
	VALUES(SOURCE.[UserId], SOURCE.[RoleId])
;
