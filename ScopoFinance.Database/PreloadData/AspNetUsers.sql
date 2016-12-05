MERGE [dbo].[AspNetUsers] AS TARGET
USING
(	
		
			SELECT @User_HOUser				, 'houser@ntitas.com'				, 1, 'AFvML2fj/FCZbuLK3BWPY2BrMb/uras7IS4R3N+Mmm99gRXXzQpCdXCCsP8HUVu9pA==', 'fd82f02b-3e8f-421a-b71a-ba0066774b95', NULL, 0, 0, NULL, 0, 0, 'houser@ntitas.com', GETDATE()
	UNION	SELECT @User_BranchUser_101		, 'branchuser101@ntitas.com'		, 1, 'AFvML2fj/FCZbuLK3BWPY2BrMb/uras7IS4R3N+Mmm99gRXXzQpCdXCCsP8HUVu9pA==', 'fd82f02b-3e8f-421a-b71a-ba0066774b95', NULL, 0, 0, NULL, 0, 0, 'branchuser101@ntitas.com', GETDATE()
	UNION	SELECT @User_BranchUser_102		, 'branchuser102@ntitas.com'		, 1, 'AFvML2fj/FCZbuLK3BWPY2BrMb/uras7IS4R3N+Mmm99gRXXzQpCdXCCsP8HUVu9pA==', 'fd82f02b-3e8f-421a-b71a-ba0066774b95', NULL, 0, 0, NULL, 0, 0, 'branchuser102@ntitas.com', GETDATE()

) AS SOURCE ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LastPasswordChangedDate])
ON TARGET.[Id] = SOURCE.[Id]
WHEN MATCHED THEN
UPDATE SET
	TARGET.[Email]                = SOURCE.[Email],
	TARGET.[EmailConfirmed]       = SOURCE.[EmailConfirmed],
	TARGET.[PasswordHash]         = SOURCE.[PasswordHash],
	TARGET.[SecurityStamp]        = SOURCE.[SecurityStamp],
	TARGET.[PhoneNumber]          = SOURCE.[PhoneNumber],
	TARGET.[PhoneNumberConfirmed] = SOURCE.[PhoneNumberConfirmed],
	TARGET.[TwoFactorEnabled]     = SOURCE.[TwoFactorEnabled],
	TARGET.[LockoutEndDateUtc]    = SOURCE.[LockoutEndDateUtc],
	TARGET.[LockoutEnabled]       = SOURCE.[LockoutEnabled],
	TARGET.[AccessFailedCount]    = SOURCE.[AccessFailedCount],
	TARGET.[UserName]             = SOURCE.[UserName],
	TARGET.[LastPasswordChangedDate] = SOURCE.[LastPasswordChangedDate]
WHEN NOT MATCHED THEN
	INSERT([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LastPasswordChangedDate])
	VALUES(SOURCE.[Id], SOURCE.[Email], SOURCE.[EmailConfirmed], SOURCE.[PasswordHash], SOURCE.[SecurityStamp], SOURCE.[PhoneNumber], SOURCE.[PhoneNumberConfirmed], SOURCE.[TwoFactorEnabled], SOURCE.[LockoutEndDateUtc], SOURCE.[LockoutEnabled], SOURCE.[AccessFailedCount], SOURCE.[UserName], SOURCE.[LastPasswordChangedDate])
;
