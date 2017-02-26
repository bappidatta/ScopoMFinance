SET IDENTITY_INSERT [lnsav].[GlobalPolicy] ON;

MERGE [lnsav].[GlobalPolicy] AS TARGET
USING
(
			SELECT 1, 3

) AS SOURCE ([Id],[NoOfMaxLoan])
	ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
	UPDATE SET
		TARGET.[NoOfMaxLoan] = SOURCE.[NoOfMaxLoan]
WHEN NOT MATCHED THEN
	INSERT([Id],[NoOfMaxLoan])
	VALUES(SOURCE.[Id],SOURCE.[NoOfMaxLoan])
;

SET IDENTITY_INSERT [lnsav].[GlobalPolicy] OFF;