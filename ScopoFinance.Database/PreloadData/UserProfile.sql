-- The developer needs to add data in the SELECT statements as they need and include this file in the PreloadData.sql

MERGE [dbo].[UserProfile] AS TARGET
USING
(
			SELECT @User_HOUser				, 'HOUser FirstName'	, 'HOUser LastName'		, '+8801990407011', 'ScopoMFinance', GETDATE()
	UNION	SELECT @User_BranchUser_101		, 'Branch101 FirstName'	, 'Branch101 LastName'	, '+8801990407012', 'ScopoMFinance', GETDATE()
	UNION	SELECT @User_BranchUser_102		, 'Branch102 FirstName'	, 'Branch102 LastName'	, '+8801990407013', 'ScopoMFinance', GETDATE()

) AS SOURCE ([UserId], [FirstName], [LastName], [MobileNo], [CreatedBy], [CreatedOn])
ON TARGET.[UserId] = SOURCE.[UserId]
WHEN MATCHED THEN
UPDATE SET
	TARGET.[FirstName] = SOURCE.[FirstName],
	TARGET.[LastName] = SOURCE.[LastName],
	TARGET.[MobileNo] = SOURCE.[MobileNo],
	TARGET.[CreatedBy] = SOURCE.[CreatedBy],
	TARGET.[CreatedOn] = SOURCE.[CreatedOn]
WHEN NOT MATCHED THEN
	INSERT([UserId], [FirstName], [LastName], [MobileNo], [CreatedBy],[CreatedOn])
	VALUES(SOURCE.[UserId], SOURCE.[FirstName], SOURCE.[LastName], SOURCE.[MobileNo], SOURCE.[CreatedBy], SOURCE.[CreatedOn])
;
