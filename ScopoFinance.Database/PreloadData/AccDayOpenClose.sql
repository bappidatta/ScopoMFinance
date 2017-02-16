SET IDENTITY_INSERT [dbo].[AccDayOpenClose] ON;

MERGE [dbo].[AccDayOpenClose] AS TARGET
USING
(
			SELECT 1, @Branch_HO , GetDate(), GetDate()
   UNION	SELECT 2, @Branch_101, GetDate(), GetDate()
   UNION	SELECT 3, @Branch_102, GetDate(), GetDate()

) AS SOURCE ([Id],[BranchId],[CurrentDate],[OpenedAt])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[BranchId] = SOURCE.[BranchId],
		TARGET.[CurrentDate] = SOURCE.[CurrentDate],
		TARGET.[OpenedAt] = SOURCE.[OpenedAt]
WHEN NOT MATCHED THEN
	INSERT([Id],[BranchId],[CurrentDate],[OpenedAt])
	VALUES(SOURCE.[Id],SOURCE.[BranchId],SOURCE.[CurrentDate],SOURCE.[OpenedAt])
;

SET IDENTITY_INSERT [dbo].[AccDayOpenClose] OFF;