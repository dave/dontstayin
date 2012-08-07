IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BacardiEmail]') AND type in (N'U'))

BEGIN
	CREATE TABLE dbo.BacardiEmail
	(
		K int NOT NULL IDENTITY (1, 1),
		Email varchar(100) NOT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.BacardiEmail ADD CONSTRAINT
PK_BacardiEmail PRIMARY KEY CLUSTERED 
(
K
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


DECLARE @vTab sql_variant 
SET @vTab = N'Emails gathered for Bacardi'
EXECUTE sp_addextendedproperty N'MS_Description', @vTab, N'SCHEMA', N'dbo', N'TABLE', N'BacardiEmail', NULL, NULL	
	
DECLARE @v0 sql_variant 
SET @v0 = N'Primary K'
EXECUTE sp_addextendedproperty N'MS_Description', @v0, N'SCHEMA', N'dbo', N'TABLE', N'BacardiEmail', N'COLUMN', N'K'

DECLARE @v1 sql_variant 
SET @v1 = N'Email'
EXECUTE sp_addextendedproperty N'MS_Description', @v1, N'SCHEMA', N'dbo', N'TABLE', N'BacardiEmail', N'COLUMN', N'Email'


END

GO
