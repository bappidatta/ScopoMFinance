SET IDENTITY_INSERT [dbo].[ComponentType] ON;

MERGE [dbo].[ComponentType] AS TARGET
USING
(
			SELECT 1, N'Core Component', 3, @User_HOUser, GetDate(), GetDate()
   UNION	SELECT 2, N'Non Core Component', 3, @User_HOUser, GetDate(), GetDate()

) AS SOURCE ([Id],[Name],[NoOfMaxLoan],[UserId],[SystemDate],[SetDate])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[Name] = SOURCE.[Name],
		TARGET.[NoOfMaxLoan] = SOURCE.[NoOfMaxLoan],
		TARGET.[UserId] = SOURCE.[UserId],
		TARGET.[SystemDate] = SOURCE.[SystemDate],
		TARGET.[SetDate] = SOURCE.[SetDate]
WHEN NOT MATCHED THEN
	INSERT([Id],[Name],[NoOfMaxLoan],[UserId],[SystemDate],[SetDate])
	VALUES(SOURCE.[Id],SOURCE.[Name],SOURCE.[NoOfMaxLoan],SOURCE.[UserId],SOURCE.[SystemDate],SOURCE.[SetDate])
;

SET IDENTITY_INSERT [dbo].[ComponentType] OFF;